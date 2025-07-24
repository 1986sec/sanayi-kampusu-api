namespace IndustrialCampusAPI.DTOs
{
    public class FirmaDTO
    {
        public int FirmaID { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public string? Adres { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
    }

    public class FirmaCreateDTO
    {
        public string FirmaAdi { get; set; } = string.Empty;
        public string? Adres { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
    }

    public class FirmaUpdateDTO
    {
        public string FirmaAdi { get; set; } = string.Empty;
        public string? Adres { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
    }
}