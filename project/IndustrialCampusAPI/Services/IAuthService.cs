using IndustrialCampusAPI.DTOs;

namespace IndustrialCampusAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(KullaniciLoginDTO loginDto);
        Task<AuthResponseDTO?> RegisterAsync(KullaniciRegisterDTO registerDto);
        string GenerateJwtToken(KullaniciDTO kullanici);
        Task<KullaniciDTO?> UpdateUserAsync(int id, KullaniciUpdateDTO updateDto);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> SifreSifirlamaTalepAsync(SifreSifirlamaTalepDTO dto);
        Task<bool> SifreSifirlamaOnayAsync(SifreSifirlamaOnayDTO dto);
        Task<bool> SifreDegistirAsync(SifreDegistirDTO dto);
        Task<RefreshTokenResponseDTO?> RefreshTokenAsync(RefreshTokenRequestDTO dto);
    }
}