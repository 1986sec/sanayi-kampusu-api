// powered by 1986sec
using IndustrialCampusAPI.Data;
using IndustrialCampusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustrialCampusAPI.Repositories
{
    public class FirmaRepository : IFirmaRepository
    {
        private readonly ApplicationDbContext _context;

        public FirmaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Firma>> GetAllAsync()
        {
            return await _context.Firmalar
                .OrderBy(f => f.FirmaAdi)
                .ToListAsync();
        }

        public async Task<Firma?> GetByIdAsync(int id)
        {
            return await _context.Firmalar
                .Include(f => f.Ziyaretler)
                .Include(f => f.GelirGiderler)
                .FirstOrDefaultAsync(f => f.FirmaID == id);
        }

        public async Task<Firma> CreateAsync(Firma firma)
        {
            _context.Firmalar.Add(firma);
            await _context.SaveChangesAsync();
            return firma;
        }

        public async Task<Firma> UpdateAsync(Firma firma)
        {
            _context.Entry(firma).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return firma;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var firma = await _context.Firmalar.FindAsync(id);
            if (firma == null) return false;

            _context.Firmalar.Remove(firma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Firma>> SearchAsync(string? searchTerm)
        {
            var query = _context.Firmalar.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(f => f.FirmaAdi.Contains(searchTerm) ||
                                       f.Email!.Contains(searchTerm) ||
                                       f.Telefon!.Contains(searchTerm));
            }

            return await query.OrderBy(f => f.FirmaAdi).ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Firmalar.AnyAsync(f => f.FirmaID == id);
        }
    }
}
// powered by 1986sec