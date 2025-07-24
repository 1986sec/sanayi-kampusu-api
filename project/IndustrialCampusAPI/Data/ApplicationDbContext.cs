// powered by 1986sec
using IndustrialCampusAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IndustrialCampusAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Ziyaret> Ziyaretler { get; set; }
        public DbSet<GelirGider> GelirGiderler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Firma configurations
            modelBuilder.Entity<Firma>()
                .HasIndex(f => f.FirmaAdi)
                .IsUnique();

            // Kullanici configurations
            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.Email)
                .IsUnique();

            // Ziyaret relationships
            modelBuilder.Entity<Ziyaret>()
                .HasOne(z => z.Firma)
                .WithMany(f => f.Ziyaretler)
                .HasForeignKey(z => z.FirmaID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ziyaret>()
                .HasOne(z => z.Kullanici)
                .WithMany(k => k.Ziyaretler)
                .HasForeignKey(z => z.KullaniciID)
                .OnDelete(DeleteBehavior.Restrict);

            // GelirGider relationships
            modelBuilder.Entity<GelirGider>()
                .HasOne(gg => gg.Firma)
                .WithMany(f => f.GelirGiderler)
                .HasForeignKey(gg => gg.FirmaID)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            modelBuilder.Entity<Kullanici>().HasData(
                new Kullanici
                {
                    KullaniciID = 1,
                    AdSoyad = "Admin User",
                    Email = "admin@campus.com",
                    SifreHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Rol = "Admin",
                    OlusturmaTarihi = DateTime.Now,
                    Aktif = true
                }
            );
        }
    }
}
// powered by 1986sec