using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialCampusAPI.Models
{
    public class Ziyaret
    {
        [Key]
        public int ZiyaretID { get; set; }
        
        [Required]
        [ForeignKey("Firma")]
        public int FirmaID { get; set; }
        
        [Required]
        [ForeignKey("Kullanici")]
        public int KullaniciID { get; set; }
        
        [Required]
        public DateTime ZiyaretTarihi { get; set; }
        
        [StringLength(200)]
        public string? Amac { get; set; }
        
        [StringLength(1000)]
        public string? Notlar { get; set; }
        
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual Firma Firma { get; set; } = null!;
        public virtual Kullanici Kullanici { get; set; } = null!;
    }
}