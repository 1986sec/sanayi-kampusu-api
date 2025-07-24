using IndustrialCampusAPI.Models;

namespace IndustrialCampusAPI.Repositories
{
    public interface IFirmaRepository
    {
        Task<IEnumerable<Firma>> GetAllAsync();
        Task<Firma?> GetByIdAsync(int id);
        Task<Firma> CreateAsync(Firma firma);
        Task<Firma> UpdateAsync(Firma firma);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Firma>> SearchAsync(string? searchTerm);
        Task<bool> ExistsAsync(int id);
    }
}