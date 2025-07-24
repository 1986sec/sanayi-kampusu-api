using AutoMapper;
using IndustrialCampusAPI.DTOs;
using IndustrialCampusAPI.Models;
using IndustrialCampusAPI.Repositories;

namespace IndustrialCampusAPI.Services
{
    public class ZiyaretService : IZiyaretService
    {
        private readonly IZiyaretRepository _ziyaretRepository;
        private readonly IMapper _mapper;

        public ZiyaretService(IZiyaretRepository ziyaretRepository, IMapper mapper)
        {
            _ziyaretRepository = ziyaretRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ZiyaretDTO>> GetAllAsync()
        {
            var ziyaretler = await _ziyaretRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ZiyaretDTO>>(ziyaretler);
        }

        public async Task<IEnumerable<ZiyaretDTO>> GetByUserIdAsync(int kullaniciId)
        {
            var ziyaretler = await _ziyaretRepository.GetByUserIdAsync(kullaniciId);
            return _mapper.Map<IEnumerable<ZiyaretDTO>>(ziyaretler);
        }

        public async Task<IEnumerable<ZiyaretDTO>> GetByFirmaIdAsync(int firmaId)
        {
            var ziyaretler = await _ziyaretRepository.GetByFirmaIdAsync(firmaId);
            return _mapper.Map<IEnumerable<ZiyaretDTO>>(ziyaretler);
        }

        public async Task<ZiyaretDTO?> GetByIdAsync(int id)
        {
            var ziyaret = await _ziyaretRepository.GetByIdAsync(id);
            return ziyaret != null ? _mapper.Map<ZiyaretDTO>(ziyaret) : null;
        }

        public async Task<ZiyaretDTO> CreateAsync(ZiyaretCreateDTO createDto, int kullaniciId)
        {
            var ziyaret = _mapper.Map<Ziyaret>(createDto);
            ziyaret.KullaniciID = kullaniciId;
            
            var createdZiyaret = await _ziyaretRepository.CreateAsync(ziyaret);
            return _mapper.Map<ZiyaretDTO>(createdZiyaret);
        }

        public async Task<ZiyaretDTO?> UpdateAsync(int id, ZiyaretUpdateDTO updateDto, int kullaniciId, string rol)
        {
            if (!await _ziyaretRepository.CanUserAccessAsync(id, kullaniciId, rol))
                return null;

            var existingZiyaret = await _ziyaretRepository.GetByIdAsync(id);
            if (existingZiyaret == null) return null;

            _mapper.Map(updateDto, existingZiyaret);
            var updatedZiyaret = await _ziyaretRepository.UpdateAsync(existingZiyaret);
            return _mapper.Map<ZiyaretDTO>(updatedZiyaret);
        }

        public async Task<bool> DeleteAsync(int id, int kullaniciId, string rol)
        {
            if (!await _ziyaretRepository.CanUserAccessAsync(id, kullaniciId, rol))
                return false;

            return await _ziyaretRepository.DeleteAsync(id);
        }
    }
}