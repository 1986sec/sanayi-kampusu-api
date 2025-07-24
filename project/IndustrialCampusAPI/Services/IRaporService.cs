namespace IndustrialCampusAPI.Services
{
    public interface IRaporService
    {
        Task<byte[]> ZiyaretRaporuPdfAsync(int firmaId);
        Task<byte[]> ZiyaretRaporuExcelAsync(int firmaId);
        Task<byte[]> GelirGiderRaporuPdfAsync(int firmaId);
        Task<byte[]> GelirGiderRaporuExcelAsync(int firmaId);
        Task<object> GetGunlukZiyaretRaporuAsync();
        Task<object> GetFirmaGelirGiderRaporuAsync(int firmaId);
    }
}