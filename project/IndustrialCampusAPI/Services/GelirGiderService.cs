using AutoMapper;
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Models;
using IndustrialCampusAPI.Repositories;

namespace IndustrialCampusAPI.Services
{
    public class GelirGiderService : IGelirGiderService
    {
        private readonly IGelirGiderRepository _gelirGiderRepository;
        private readonly IMapper _mapper;

        public GelirGiderService(IGelirGiderRepository gelirGiderRepository, IMapper mapper)
        {
            _gelirGiderRepository = gelirGiderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GelirGiderDTO>> GetAllAsync()
        {
            var gelirGiderler = await _gelirGiderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GelirGiderDTO>>(gelirGiderler);
        }

        public async Task<IEnumerable<GelirGiderDTO>> GetByFirmaIdAsync(int firmaId)
        {
            var gelirGiderler = await _gelirGiderRepository.GetByFirmaIdAsync(firmaId);
            return _mapper.Map<IEnumerable<GelirGiderDTO>>(gelirGiderler);
        }

        public async Task<GelirGiderDTO?> GetByIdAsync(int id)
        {
            var gelirGider = await _gelirGiderRepository.GetByIdAsync(id);
            return gelirGider != null ? _mapper.Map<GelirGiderDTO>(gelirGider) : null;
        }

        public async Task<GelirGiderDTO> CreateAsync(GelirGiderCreateDTO createDto)
        {
            var gelirGider = _mapper.Map<GelirGider>(createDto);
            var createdGelirGider = await _gelirGiderRepository.CreateAsync(gelirGider);
            return _mapper.Map<GelirGiderDTO>(createdGelirGider);
        }

        public async Task<GelirGiderDTO?> UpdateAsync(int id, GelirGiderUpdateDTO updateDto)
        {
            var existingGelirGider = await _gelirGiderRepository.GetByIdAsync(id);
            if (existingGelirGider == null) return null;

            _mapper.Map(updateDto, existingGelirGider);
            var updatedGelirGider = await _gelirGiderRepository.UpdateAsync(existingGelirGider);
            return _mapper.Map<GelirGiderDTO>(updatedGelirGider);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _gelirGiderRepository.DeleteAsync(id);
        }

        public async Task<KarZararDTO> GetKarZararAsync(int? firmaId, DateTime? baslangicTarihi, DateTime? bitisTarihi)
        {
            var (toplamGelir, toplamGider) = await _gelirGiderRepository.GetKarZararAsync(firmaId, baslangicTarihi, bitisTarihi);
            
            return new KarZararDTO
            {
                ToplamGelir = toplamGelir,
                ToplamGider = toplamGider,
                NetKarZarar = toplamGelir - toplamGider,
                BaslangicTarihi = baslangicTarihi ?? DateTime.MinValue,
                BitisTarihi = bitisTarihi ?? DateTime.MaxValue
            };
        }
    }
}