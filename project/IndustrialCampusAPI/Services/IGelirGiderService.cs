using IndustrialCampusAPI.DTOs;

namespace IndustrialCampusAPI.Services
{
    public interface IGelirGiderService
    {
        Task<IEnumerable<GelirGiderDTO>> GetAllAsync();
        Task<IEnumerable<GelirGiderDTO>> GetByFirmaIdAsync(int firmaId);
        Task<GelirGiderDTO?> GetByIdAsync(int id);
        Task<GelirGiderDTO> CreateAsync(GelirGiderCreateDTO createDto);
        Task<GelirGiderDTO?> UpdateAsync(int id, GelirGiderUpdateDTO updateDto);
        Task<bool> DeleteAsync(int id);
        Task<KarZararDTO> GetKarZararAsync(int? firmaId, DateTime? baslangicTarihi, DateTime? bitisTarihi);
    }
}