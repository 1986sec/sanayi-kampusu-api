using IndustrialCampusAPI.Models;

namespace IndustrialCampusAPI.Repositories
{
    public interface IKullaniciRepository
    {
        Task<Kullanici?> GetByEmailAsync(string email);
        Task<Kullanici?> GetByIdAsync(int id);
        Task<Kullanici> CreateAsync(Kullanici kullanici);
        Task<bool> EmailExistsAsync(string email);
        Task<IEnumerable<Kullanici>> GetAllAsync();
        Task<Kullanici> UpdateAsync(Kullanici kullanici);
        Task<bool> DeleteAsync(int id);
        Task<Kullanici?> GetBySifreSifirlamaTokenAsync(string token);
        Task ClearSifreSifirlamaTokenAsync(int kullaniciId);
    }
}