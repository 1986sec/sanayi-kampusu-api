using IndustrialCampusAPI.Models;

namespace IndustrialCampusAPI.Repositories
{
    public interface IGelirGiderRepository
    {
        Task<IEnumerable<GelirGider>> GetAllAsync();
        Task<IEnumerable<GelirGider>> GetByFirmaIdAsync(int firmaId);
        Task<GelirGider?> GetByIdAsync(int id);
        Task<GelirGider> CreateAsync(GelirGider gelirGider);
        Task<GelirGider> UpdateAsync(GelirGider gelirGider);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<(decimal toplamGelir, decimal toplamGider)> GetKarZararAsync(int? firmaId, DateTime? baslangicTarihi, DateTime? bitisTarihi);
    }
}