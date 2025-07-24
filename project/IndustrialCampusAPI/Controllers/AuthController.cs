// powered by 1986sec
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IndustrialCampusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] KullaniciLoginDTO loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
            {
                _logger.LogWarning("Başarısız giriş denemesi. Email: {Email}", loginDto.Email);
                return Unauthorized("Geçersiz email veya şifre");
            }
            _logger.LogInformation("Kullanıcı giriş yaptı. Email: {Email}", loginDto.Email);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register([FromBody] KullaniciRegisterDTO registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (result == null)
            {
                _logger.LogWarning("Başarısız kayıt denemesi. Email zaten kullanılıyor: {Email}", registerDto.Email);
                return BadRequest("Bu email adresi zaten kullanılıyor");
            }
            _logger.LogInformation("Yeni kullanıcı kaydı başarılı. Email: {Email}", registerDto.Email);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<KullaniciDTO>> UpdateUser(int id, [FromBody] KullaniciUpdateDTO updateDto)
        {
            var result = await _authService.UpdateUserAsync(id, updateDto);
            if (result == null)
            {
                _logger.LogWarning("Kullanıcı güncelleme başarısız. ID: {Id}", id);
                return NotFound("Kullanıcı bulunamadı");
            }
            _logger.LogInformation("Kullanıcı güncellendi. ID: {Id}", id);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _authService.DeleteUserAsync(id);
            if (!success)
            {
                _logger.LogWarning("Kullanıcı silme başarısız. ID: {Id}", id);
                return NotFound("Kullanıcı bulunamadı veya zaten silinmiş");
            }
            _logger.LogInformation("Kullanıcı silindi. ID: {Id}", id);
            return NoContent();
        }

        [HttpPost("sifre-sifirlama-talep")]
        public async Task<IActionResult> SifreSifirlamaTalep([FromBody] SifreSifirlamaTalepDTO dto)
        {
            var success = await _authService.SifreSifirlamaTalepAsync(dto);
            if (!success)
            {
                _logger.LogWarning("Şifre sıfırlama talebi başarısız. Email: {Email}", dto.Email);
                return NotFound("Kullanıcı bulunamadı");
            }
            _logger.LogInformation("Şifre sıfırlama talebi başarılı. Email: {Email}", dto.Email);
            return Ok("Şifre sıfırlama talimatı e-posta adresinize gönderildi.");
        }

        [HttpPost("sifre-sifirlama-onay")]
        public async Task<IActionResult> SifreSifirlamaOnay([FromBody] SifreSifirlamaOnayDTO dto)
        {
            var success = await _authService.SifreSifirlamaOnayAsync(dto);
            if (!success)
            {
                _logger.LogWarning("Şifre sıfırlama onayı başarısız. Email: {Email}", dto.Email);
                return BadRequest("Geçersiz veya süresi dolmuş token.");
            }
            _logger.LogInformation("Şifre sıfırlama onayı başarılı. Email: {Email}", dto.Email);
            return Ok("Şifreniz başarıyla güncellendi.");
        }

        [HttpPost("sifre-degistir")]
        public async Task<IActionResult> SifreDegistir([FromBody] SifreDegistirDTO dto)
        {
            var success = await _authService.SifreDegistirAsync(dto);
            if (!success)
            {
                _logger.LogWarning("Şifre değiştirme başarısız. KullanıcıID: {Id}", dto.KullaniciID);
                return BadRequest("Mevcut şifre hatalı veya kullanıcı bulunamadı.");
            }
            _logger.LogInformation("Şifre değiştirme başarılı. KullanıcıID: {Id}", dto.KullaniciID);
            return Ok("Şifreniz başarıyla değiştirildi.");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<RefreshTokenResponseDTO>> RefreshToken([FromBody] RefreshTokenRequestDTO dto)
        {
            var result = await _authService.RefreshTokenAsync(dto);
            if (result == null)
            {
                _logger.LogWarning("Refresh token başarısız. Token: {Token}", dto.RefreshToken);
                return Unauthorized("Geçersiz veya süresi dolmuş refresh token.");
            }
            _logger.LogInformation("Refresh token başarılı. KullanıcıID: {Id}", result.Kullanici.KullaniciID);
            return Ok(result);
        }
    }
}
// powered by 1986sec