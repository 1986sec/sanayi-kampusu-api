// powered by 1986sec
using AutoMapper;
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Models;
using IndustrialCampusAPI.Repositories;

namespace IndustrialCampusAPI.Services
{
    public class FirmaService : IFirmaService
    {
        private readonly IFirmaRepository _firmaRepository;
        private readonly IMapper _mapper;

        public FirmaService(IFirmaRepository firmaRepository, IMapper mapper)
        {
            _firmaRepository = firmaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FirmaDTO>> GetAllAsync()
        {
            var firmalar = await _firmaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FirmaDTO>>(firmalar);
        }

        public async Task<FirmaDTO?> GetByIdAsync(int id)
        {
            var firma = await _firmaRepository.GetByIdAsync(id);
            return firma != null ? _mapper.Map<FirmaDTO>(firma) : null;
        }

        public async Task<FirmaDTO> CreateAsync(FirmaCreateDTO createDto)
        {
            var firma = _mapper.Map<Firma>(createDto);
            var createdFirma = await _firmaRepository.CreateAsync(firma);
            return _mapper.Map<FirmaDTO>(createdFirma);
        }

        public async Task<FirmaDTO?> UpdateAsync(int id, FirmaUpdateDTO updateDto)
        {
            var existingFirma = await _firmaRepository.GetByIdAsync(id);
            if (existingFirma == null) return null;

            _mapper.Map(updateDto, existingFirma);
            var updatedFirma = await _firmaRepository.UpdateAsync(existingFirma);
            return _mapper.Map<FirmaDTO>(updatedFirma);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _firmaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FirmaDTO>> SearchAsync(string? searchTerm)
        {
            var firmalar = await _firmaRepository.SearchAsync(searchTerm);
            return _mapper.Map<IEnumerable<FirmaDTO>>(firmalar);
        }
    }
}
// powered by 1986sec