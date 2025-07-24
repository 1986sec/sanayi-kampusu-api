using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialCampusAPI.Models
{
    public class GelirGider
    {
        [Key]
        public int IslemID { get; set; }
        
        [Required]
        [ForeignKey("Firma")]
        public int FirmaID { get; set; }
        
        [Required]
        public DateTime Tarih { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tutar { get; set; }
        
        [Required]
        [StringLength(10)]
        public string IslemTipi { get; set; } = string.Empty; // Gelir, Gider
        
        [StringLength(500)]
        public string? Aciklama { get; set; }
        
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        // Navigation Properties
        public virtual Firma Firma { get; set; } = null!;
    }
}