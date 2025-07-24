using IndustrialCampusAPI.Models;

namespace IndustrialCampusAPI.Repositories
{
    public interface IZiyaretRepository
    {
        Task<IEnumerable<Ziyaret>> GetAllAsync();
        Task<IEnumerable<Ziyaret>> GetByUserIdAsync(int kullaniciId);
        Task<IEnumerable<Ziyaret>> GetByFirmaIdAsync(int firmaId);
        Task<Ziyaret?> GetByIdAsync(int id);
        Task<Ziyaret> CreateAsync(Ziyaret ziyaret);
        Task<Ziyaret> UpdateAsync(Ziyaret ziyaret);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> CanUserAccessAsync(int ziyaretId, int kullaniciId, string rol);
    }
}