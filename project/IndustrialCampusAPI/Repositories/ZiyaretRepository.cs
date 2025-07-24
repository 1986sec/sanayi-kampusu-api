using IndustrialCampusAPI.Data;
using IndustrialCampusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustrialCampusAPI.Repositories
{
    public class ZiyaretRepository : IZiyaretRepository
    {
        private readonly ApplicationDbContext _context;

        public ZiyaretRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ziyaret>> GetAllAsync()
        {
            return await _context.Ziyaretler
                .Include(z => z.Firma)
                .Include(z => z.Kullanici)
                .OrderByDescending(z => z.ZiyaretTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ziyaret>> GetByUserIdAsync(int kullaniciId)
        {
            return await _context.Ziyaretler
                .Include(z => z.Firma)
                .Include(z => z.Kullanici)
                .Where(z => z.KullaniciID == kullaniciId)
                .OrderByDescending(z => z.ZiyaretTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ziyaret>> GetByFirmaIdAsync(int firmaId)
        {
            return await _context.Ziyaretler
                .Include(z => z.Firma)
                .Include(z => z.Kullanici)
                .Where(z => z.FirmaID == firmaId)
                .OrderByDescending(z => z.ZiyaretTarihi)
                .ToListAsync();
        }

        public async Task<Ziyaret?> GetByIdAsync(int id)
        {
            return await _context.Ziyaretler
                .Include(z => z.Firma)
                .Include(z => z.Kullanici)
                .FirstOrDefaultAsync(z => z.ZiyaretID == id);
        }

        public async Task<Ziyaret> CreateAsync(Ziyaret ziyaret)
        {
            _context.Ziyaretler.Add(ziyaret);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(ziyaret.ZiyaretID) ?? ziyaret;
        }

        public async Task<Ziyaret> UpdateAsync(Ziyaret ziyaret)
        {
            _context.Entry(ziyaret).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetByIdAsync(ziyaret.ZiyaretID) ?? ziyaret;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ziyaret = await _context.Ziyaretler.FindAsync(id);
            if (ziyaret == null) return false;

            _context.Ziyaretler.Remove(ziyaret);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Ziyaretler.AnyAsync(z => z.ZiyaretID == id);
        }

        public async Task<bool> CanUserAccessAsync(int ziyaretId, int kullaniciId, string rol)
        {
            if (rol == "Admin") return true;

            return await _context.Ziyaretler
                .AnyAsync(z => z.ZiyaretID == ziyaretId && z.KullaniciID == kullaniciId);
        }
    }
}