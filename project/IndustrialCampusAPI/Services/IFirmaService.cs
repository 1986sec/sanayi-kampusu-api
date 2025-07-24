using IndustrialCampusAPI.DTOs;

namespace IndustrialCampusAPI.Services
{
    public interface IFirmaService
    {
        Task<IEnumerable<FirmaDTO>> GetAllAsync();
        Task<FirmaDTO?> GetByIdAsync(int id);
        Task<FirmaDTO> CreateAsync(FirmaCreateDTO createDto);
        Task<FirmaDTO?> UpdateAsync(int id, FirmaUpdateDTO updateDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<FirmaDTO>> SearchAsync(string? searchTerm);
    }
}