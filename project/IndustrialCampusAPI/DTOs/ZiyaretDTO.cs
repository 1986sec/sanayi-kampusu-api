namespace IndustrialCampusAPI.DTOs
{
    public class ZiyaretDTO
    {
        public int ZiyaretID { get; set; }
        public int FirmaID { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; } = string.Empty;
        public DateTime ZiyaretTarihi { get; set; }
        public string? Amac { get; set; }
        public string? Notlar { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
    }

    public class ZiyaretCreateDTO
    {
        public int FirmaID { get; set; }
        public DateTime ZiyaretTarihi { get; set; }
        public string? Amac { get; set; }
        public string? Notlar { get; set; }
    }

    public class ZiyaretUpdateDTO
    {
        public DateTime ZiyaretTarihi { get; set; }
        public string? Amac { get; set; }
        public string? Notlar { get; set; }
    }
}