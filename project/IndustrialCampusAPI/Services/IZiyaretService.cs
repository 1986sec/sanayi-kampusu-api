using IndustrialCampusAPI.DTOs;

namespace IndustrialCampusAPI.Services
{
    public interface IZiyaretService
    {
        Task<IEnumerable<ZiyaretDTO>> GetAllAsync();
        Task<IEnumerable<ZiyaretDTO>> GetByUserIdAsync(int kullaniciId);
        Task<IEnumerable<ZiyaretDTO>> GetByFirmaIdAsync(int firmaId);
        Task<ZiyaretDTO?> GetByIdAsync(int id);
        Task<ZiyaretDTO> CreateAsync(ZiyaretCreateDTO createDto, int kullaniciId);
        Task<ZiyaretDTO?> UpdateAsync(int id, ZiyaretUpdateDTO updateDto, int kullaniciId, string rol);
        Task<bool> DeleteAsync(int id, int kullaniciId, string rol);
    }
}