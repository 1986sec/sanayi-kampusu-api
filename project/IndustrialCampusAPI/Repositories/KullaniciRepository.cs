// powered by 1986sec
using IndustrialCampusAPI.Data;
using IndustrialCampusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustrialCampusAPI.Repositories
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly ApplicationDbContext _context;

        public KullaniciRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Kullanici?> GetByEmailAsync(string email)
        {
            return await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == email && k.Aktif);
        }

        public async Task<Kullanici?> GetByIdAsync(int id)
        {
            return await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.KullaniciID == id && k.Aktif);
        }

        public async Task<Kullanici?> GetBySifreSifirlamaTokenAsync(string token)
        {
            return await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.SifreSifirlamaToken == token && k.SifreSifirlamaTokenSonKullanma > DateTime.UtcNow && k.Aktif);
        }

        public async Task ClearSifreSifirlamaTokenAsync(int kullaniciId)
        {
            var kullanici = await _context.Kullanicilar.FirstOrDefaultAsync(k => k.KullaniciID == kullaniciId);
            if (kullanici != null)
            {
                kullanici.SifreSifirlamaToken = null;
                kullanici.SifreSifirlamaTokenSonKullanma = null;
                _context.Kullanicilar.Update(kullanici);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Kullanici> CreateAsync(Kullanici kullanici)
        {
            _context.Kullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();
            return kullanici;
        }

        public async Task<Kullanici> UpdateAsync(Kullanici kullanici)
        {
            _context.Kullanicilar.Update(kullanici);
            await _context.SaveChangesAsync();
            return kullanici;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var kullanici = await _context.Kullanicilar.FirstOrDefaultAsync(k => k.KullaniciID == id && k.Aktif);
            if (kullanici == null)
                return false;
            kullanici.Aktif = false;
            _context.Kullanicilar.Update(kullanici);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Kullanicilar
                .AnyAsync(k => k.Email == email);
        }

        public async Task<IEnumerable<Kullanici>> GetAllAsync()
        {
            return await _context.Kullanicilar
                .Where(k => k.Aktif)
                .OrderBy(k => k.AdSoyad)
                .ToListAsync();
        }
    }
}
// powered by 1986sec