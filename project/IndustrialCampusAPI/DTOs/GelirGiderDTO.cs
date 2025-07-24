// powered by 1986sec
namespace IndustrialCampusAPI.DTOs
{
    public class GelirGiderDTO
    {
        public int IslemID { get; set; }
        public int FirmaID { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
        public string IslemTipi { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public int GelirGiderID { get; set; }
    }

    public class GelirGiderCreateDTO
    {
        public int FirmaID { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
        public string IslemTipi { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
    }

    public class GelirGiderUpdateDTO
    {
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
        public string IslemTipi { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
    }

    public class KarZararDTO
    {
        public decimal ToplamGelir { get; set; }
        public decimal ToplamGider { get; set; }
        public decimal NetKarZarar { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}
// powered by 1986sec