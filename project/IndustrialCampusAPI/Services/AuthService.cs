// powered by 1986sec
using AutoMapper;
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Models;
using IndustrialCampusAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IndustrialCampusAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(IKullaniciRepository kullaniciRepository, IMapper mapper, IConfiguration configuration, ILogger<AuthService> logger, IEmailService emailService)
        {
            _kullaniciRepository = kullaniciRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<AuthResponseDTO?> LoginAsync(KullaniciLoginDTO loginDto)
        {
            var kullanici = await _kullaniciRepository.GetByEmailAsync(loginDto.Email);
            
            if (kullanici == null || !BCrypt.Net.BCrypt.Verify(loginDto.Sifre, kullanici.SifreHash))
            {
                _logger.LogWarning("Login başarısız. Email: {Email}", loginDto.Email);
                return null;
            }

            // Refresh token üret ve kaydet
            var refreshToken = GenerateRefreshToken();
            kullanici.RefreshToken = refreshToken;
            kullanici.RefreshTokenSonKullanma = DateTime.UtcNow.AddDays(7);
            await _kullaniciRepository.UpdateAsync(kullanici);

            _logger.LogInformation("Login başarılı. Email: {Email}", loginDto.Email);
            var kullaniciDto = _mapper.Map<KullaniciDTO>(kullanici);
            var token = GenerateJwtToken(kullaniciDto);

            return new AuthResponseDTO
            {
                Token = token,
                Kullanici = kullaniciDto
            };
        }

        public async Task<AuthResponseDTO?> RegisterAsync(KullaniciRegisterDTO registerDto)
        {
            if (await _kullaniciRepository.EmailExistsAsync(registerDto.Email))
            {
                _logger.LogWarning("Kayıt başarısız. Email zaten kullanılıyor: {Email}", registerDto.Email);
                return null;
            }

            var refreshToken = GenerateRefreshToken();
            var kullanici = new Kullanici
            {
                AdSoyad = registerDto.AdSoyad,
                Email = registerDto.Email,
                SifreHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Sifre),
                Rol = registerDto.Rol,
                RefreshToken = refreshToken,
                RefreshTokenSonKullanma = DateTime.UtcNow.AddDays(7)
            };

            await _kullaniciRepository.CreateAsync(kullanici);

            _logger.LogInformation("Kayıt başarılı. Email: {Email}", registerDto.Email);
            var kullaniciDto = _mapper.Map<KullaniciDTO>(kullanici);
            var token = GenerateJwtToken(kullaniciDto);

            return new AuthResponseDTO
            {
                Token = token,
                Kullanici = kullaniciDto
            };
        }

        public async Task<KullaniciDTO?> UpdateUserAsync(int id, KullaniciUpdateDTO updateDto)
        {
            var kullanici = await _kullaniciRepository.GetByIdAsync(id);
            if (kullanici == null)
            {
                _logger.LogWarning("Kullanıcı güncelleme başarısız. ID: {Id}", id);
                return null;
            }

            kullanici.AdSoyad = updateDto.AdSoyad;
            kullanici.Email = updateDto.Email;
            kullanici.Rol = updateDto.Rol;
            kullanici.Aktif = updateDto.Aktif;

            await _kullaniciRepository.UpdateAsync(kullanici);
            _logger.LogInformation("Kullanıcı güncellendi. ID: {Id}", id);
            return _mapper.Map<KullaniciDTO>(kullanici);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var result = await _kullaniciRepository.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Kullanıcı silme başarısız. ID: {Id}", id);
                return false;
            }
            _logger.LogInformation("Kullanıcı silindi. ID: {Id}", id);
            return true;
        }

        public async Task<bool> SifreSifirlamaTalepAsync(SifreSifirlamaTalepDTO dto)
        {
            var kullanici = await _kullaniciRepository.GetByEmailAsync(dto.Email);
            if (kullanici == null)
            {
                _logger.LogWarning("Şifre sıfırlama talebi başarısız. Email bulunamadı: {Email}", dto.Email);
                return false;
            }
            // Token üret
            var token = Guid.NewGuid().ToString("N");
            kullanici.SifreSifirlamaToken = token;
            kullanici.SifreSifirlamaTokenSonKullanma = DateTime.UtcNow.AddHours(1);
            await _kullaniciRepository.UpdateAsync(kullanici);
            _logger.LogInformation("Şifre sıfırlama tokenı oluşturuldu. Email: {Email}, Token: {Token}", dto.Email, token);
            // E-posta gönderimi
            var resetUrl = _configuration["Frontend:PasswordResetUrl"] ?? "https://your-frontend-domain.com/reset-password";
            var link = $"{resetUrl}?email={Uri.EscapeDataString(dto.Email)}&token={token}";
            var subject = "Şifre Sıfırlama Talebi";
            var body = $"<p>Şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklayın:</p><p><a href='{link}'>{link}</a></p><p>Bu bağlantı 1 saat boyunca geçerlidir.</p>";
            await _emailService.SendEmailAsync(dto.Email, subject, body);
            return true;
        }

        public async Task<bool> SifreSifirlamaOnayAsync(SifreSifirlamaOnayDTO dto)
        {
            var kullanici = await _kullaniciRepository.GetBySifreSifirlamaTokenAsync(dto.Token);
            if (kullanici == null || kullanici.Email != dto.Email)
            {
                _logger.LogWarning("Şifre sıfırlama onayı başarısız. Geçersiz token veya email. Email: {Email}", dto.Email);
                return false;
            }
            kullanici.SifreHash = BCrypt.Net.BCrypt.HashPassword(dto.YeniSifre);
            kullanici.SifreSifirlamaToken = null;
            kullanici.SifreSifirlamaTokenSonKullanma = null;
            await _kullaniciRepository.UpdateAsync(kullanici);
            _logger.LogInformation("Şifre sıfırlama başarılı. Email: {Email}", dto.Email);
            return true;
        }

        public async Task<bool> SifreDegistirAsync(SifreDegistirDTO dto)
        {
            var kullanici = await _kullaniciRepository.GetByIdAsync(dto.KullaniciID);
            if (kullanici == null || !BCrypt.Net.BCrypt.Verify(dto.MevcutSifre, kullanici.SifreHash))
            {
                _logger.LogWarning("Şifre değiştirme başarısız. KullanıcıID: {Id}", dto.KullaniciID);
                return false;
            }
            kullanici.SifreHash = BCrypt.Net.BCrypt.HashPassword(dto.YeniSifre);
            await _kullaniciRepository.UpdateAsync(kullanici);
            _logger.LogInformation("Şifre değiştirildi. KullanıcıID: {Id}", dto.KullaniciID);
            return true;
        }

        public async Task<RefreshTokenResponseDTO?> RefreshTokenAsync(RefreshTokenRequestDTO dto)
        {
            var kullanici = (await _kullaniciRepository.GetAllAsync()).FirstOrDefault(k => k.RefreshToken == dto.RefreshToken && k.RefreshTokenSonKullanma > DateTime.UtcNow && k.Aktif);
            if (kullanici == null)
            {
                _logger.LogWarning("Refresh token başarısız. Geçersiz veya süresi dolmuş token.");
                return null;
            }
            // Yeni access ve refresh token üret
            var yeniRefreshToken = GenerateRefreshToken();
            kullanici.RefreshToken = yeniRefreshToken;
            kullanici.RefreshTokenSonKullanma = DateTime.UtcNow.AddDays(7);
            await _kullaniciRepository.UpdateAsync(kullanici);
            var kullaniciDto = _mapper.Map<KullaniciDTO>(kullanici);
            var token = GenerateJwtToken(kullaniciDto);
            _logger.LogInformation("Refresh token başarılı. KullanıcıID: {Id}", kullanici.KullaniciID);
            return new RefreshTokenResponseDTO
            {
                Token = token,
                RefreshToken = yeniRefreshToken,
                Kullanici = kullaniciDto
            };
        }

        public string GenerateJwtToken(KullaniciDTO kullanici)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "YourSuperSecretKeyHere");
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciID.ToString()),
                    new Claim(ClaimTypes.Name, kullanici.AdSoyad),
                    new Claim(ClaimTypes.Email, kullanici.Email),
                    new Claim(ClaimTypes.Role, kullanici.Rol)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
// powered by 1986sec