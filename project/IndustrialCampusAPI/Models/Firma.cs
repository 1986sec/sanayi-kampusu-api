// powered by 1986sec
using System.ComponentModel.DataAnnotations;

namespace IndustrialCampusAPI.Models
{
    public class Firma
    {
        [Key]
        public int FirmaID { get; set; }
        
        [Required]
        [StringLength(200)]
        public string FirmaAdi { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Adres { get; set; }
        
        [StringLength(20)]
        public string? Telefon { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual ICollection<Ziyaret> Ziyaretler { get; set; } = new List<Ziyaret>();
        public virtual ICollection<GelirGider> GelirGiderler { get; set; } = new List<GelirGider>();
    }
}
// powered by 1986sec