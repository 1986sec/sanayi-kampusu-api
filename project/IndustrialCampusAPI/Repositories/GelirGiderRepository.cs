// powered by 1986sec
using IndustrialCampusAPI.Data;
using IndustrialCampusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustrialCampusAPI.Repositories
{
    public class GelirGiderRepository : IGelirGiderRepository
    {
        private readonly ApplicationDbContext _context;

        public GelirGiderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GelirGider>> GetAllAsync()
        {
            return await _context.GelirGiderler
                .Include(gg => gg.Firma)
                .OrderByDescending(gg => gg.Tarih)
                .ToListAsync();
        }

        public async Task<IEnumerable<GelirGider>> GetByFirmaIdAsync(int firmaId)
        {
            return await _context.GelirGiderler
                .Include(gg => gg.Firma)
                .Where(gg => gg.FirmaID == firmaId)
                .OrderByDescending(gg => gg.Tarih)
                .ToListAsync();
        }

        public async Task<GelirGider?> GetByIdAsync(int id)
        {
            return await _context.GelirGiderler
                .Include(gg => gg.Firma)
                .FirstOrDefaultAsync(gg => gg.IslemID == id);
        }

        public async Task<GelirGider> CreateAsync(GelirGider gelirGider)
        {
            _context.GelirGiderler.Add(gelirGider);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(gelirGider.IslemID) ?? gelirGider;
        }

        public async Task<GelirGider> UpdateAsync(GelirGider gelirGider)
        {
            _context.Entry(gelirGider).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetByIdAsync(gelirGider.IslemID) ?? gelirGider;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gelirGider = await _context.GelirGiderler.FindAsync(id);
            if (gelirGider == null) return false;

            _context.GelirGiderler.Remove(gelirGider);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.GelirGiderler.AnyAsync(gg => gg.IslemID == id);
        }

        public async Task<(decimal toplamGelir, decimal toplamGider)> GetKarZararAsync(int? firmaId, DateTime? baslangicTarihi, DateTime? bitisTarihi)
        {
            var query = _context.GelirGiderler.AsQueryable();

            if (firmaId.HasValue)
                query = query.Where(gg => gg.FirmaID == firmaId);

            if (baslangicTarihi.HasValue)
                query = query.Where(gg => gg.Tarih >= baslangicTarihi);

            if (bitisTarihi.HasValue)
                query = query.Where(gg => gg.Tarih <= bitisTarihi);

            var toplamGelir = await query
                .Where(gg => gg.IslemTipi == "Gelir")
                .SumAsync(gg => gg.Tutar);

            var toplamGider = await query
                .Where(gg => gg.IslemTipi == "Gider")
                .SumAsync(gg => gg.Tutar);

            return (toplamGelir, toplamGider);
        }
    }
}
// powered by 1986sec