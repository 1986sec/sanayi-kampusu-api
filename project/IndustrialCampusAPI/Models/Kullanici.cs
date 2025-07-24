// powered by 1986sec
using System.ComponentModel.DataAnnotations;

namespace IndustrialCampusAPI.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciID { get; set; }
        
        [Required]
        [StringLength(100)]
        public string AdSoyad { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string SifreHash { get; set; } = string.Empty;
        
        public string? SifreSifirlamaToken { get; set; }
        public DateTime? SifreSifirlamaTokenSonKullanma { get; set; }
        
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenSonKullanma { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = "Personel"; // Admin, Personel
        
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        public bool Aktif { get; set; } = true;
        
        // Navigation Properties
        public virtual ICollection<Ziyaret> Ziyaretler { get; set; } = new List<Ziyaret>();
    }
}
// powered by 1986sec