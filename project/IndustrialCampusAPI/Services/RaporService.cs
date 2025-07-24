// powered by 1986sec
using IndustrialCampusAPI.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.Text;

namespace IndustrialCampusAPI.Services
{
    public class RaporService : IRaporService
    {
        private readonly IZiyaretRepository _ziyaretRepository;
        private readonly IGelirGiderRepository _gelirGiderRepository;
        private readonly IFirmaRepository _firmaRepository;

        public RaporService(IZiyaretRepository ziyaretRepository, IGelirGiderRepository gelirGiderRepository, IFirmaRepository firmaRepository)
        {
            _ziyaretRepository = ziyaretRepository;
            _gelirGiderRepository = gelirGiderRepository;
            _firmaRepository = firmaRepository;
        }

        public async Task<byte[]> ZiyaretRaporuPdfAsync(int firmaId)
        {
            var firma = await _firmaRepository.GetByIdAsync(firmaId);
            var ziyaretler = await _ziyaretRepository.GetByFirmaIdAsync(firmaId);

            using var stream = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Başlık
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var title = new Paragraph($"Ziyaret Raporu - {firma?.FirmaAdi}", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Paragraph("\n"));

            // Tablo
            var table = new PdfPTable(4);
            table.AddCell("Tarih");
            table.AddCell("Kullanıcı");
            table.AddCell("Amaç");
            table.AddCell("Notlar");

            foreach (var ziyaret in ziyaretler)
            {
                table.AddCell(ziyaret.ZiyaretTarihi.ToString("dd.MM.yyyy"));
                table.AddCell(ziyaret.Kullanici.AdSoyad);
                table.AddCell(ziyaret.Amac ?? "");
                table.AddCell(ziyaret.Notlar ?? "");
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        public async Task<byte[]> ZiyaretRaporuExcelAsync(int firmaId)
        {
            var firma = await _firmaRepository.GetByIdAsync(firmaId);
            var ziyaretler = await _ziyaretRepository.GetByFirmaIdAsync(firmaId);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Ziyaret Raporu");

            worksheet.Cells[1, 1].Value = $"Ziyaret Raporu - {firma?.FirmaAdi}";
            worksheet.Cells[1, 1, 1, 4].Merge = true;

            worksheet.Cells[3, 1].Value = "Tarih";
            worksheet.Cells[3, 2].Value = "Kullanıcı";
            worksheet.Cells[3, 3].Value = "Amaç";
            worksheet.Cells[3, 4].Value = "Notlar";

            var row = 4;
            foreach (var ziyaret in ziyaretler)
            {
                worksheet.Cells[row, 1].Value = ziyaret.ZiyaretTarihi.ToString("dd.MM.yyyy");
                worksheet.Cells[row, 2].Value = ziyaret.Kullanici.AdSoyad;
                worksheet.Cells[row, 3].Value = ziyaret.Amac ?? "";
                worksheet.Cells[row, 4].Value = ziyaret.Notlar ?? "";
                row++;
            }

            return package.GetAsByteArray();
        }

        public async Task<byte[]> GelirGiderRaporuPdfAsync(int firmaId)
        {
            var firma = await _firmaRepository.GetByIdAsync(firmaId);
            var gelirGiderler = await _gelirGiderRepository.GetByFirmaIdAsync(firmaId);

            using var stream = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Başlık
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var title = new Paragraph($"Gelir-Gider Raporu - {firma?.FirmaAdi}", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Paragraph("\n"));

            // Tablo
            var table = new PdfPTable(4);
            table.AddCell("Tarih");
            table.AddCell("İşlem Tipi");
            table.AddCell("Tutar");
            table.AddCell("Açıklama");

            foreach (var item in gelirGiderler)
            {
                table.AddCell(item.Tarih.ToString("dd.MM.yyyy"));
                table.AddCell(item.IslemTipi);
                table.AddCell($"{item.Tutar:C}");
                table.AddCell(item.Aciklama ?? "");
            }

            document.Add(table);
            document.Close();

            return stream.ToArray();
        }

        public async Task<byte[]> GelirGiderRaporuExcelAsync(int firmaId)
        {
            var firma = await _firmaRepository.GetByIdAsync(firmaId);
            var gelirGiderler = await _gelirGiderRepository.GetByFirmaIdAsync(firmaId);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Gelir-Gider Raporu");

            worksheet.Cells[1, 1].Value = $"Gelir-Gider Raporu - {firma?.FirmaAdi}";
            worksheet.Cells[1, 1, 1, 4].Merge = true;

            worksheet.Cells[3, 1].Value = "Tarih";
            worksheet.Cells[3, 2].Value = "İşlem Tipi";
            worksheet.Cells[3, 3].Value = "Tutar";
            worksheet.Cells[3, 4].Value = "Açıklama";

            var row = 4;
            foreach (var item in gelirGiderler)
            {
                worksheet.Cells[row, 1].Value = item.Tarih.ToString("dd.MM.yyyy");
                worksheet.Cells[row, 2].Value = item.IslemTipi;
                worksheet.Cells[row, 3].Value = item.Tutar;
                worksheet.Cells[row, 4].Value = item.Aciklama ?? "";
                row++;
            }

            return package.GetAsByteArray();
        }

        public Task<object> GetGunlukZiyaretRaporuAsync()
        {
            // TODO: Gerçek rapor mantığı eklenecek
            return Task.FromResult<object>(new { Success = true, Message = "Gunluk ziyaret raporu" });
        }

        public Task<object> GetFirmaGelirGiderRaporuAsync(int firmaId)
        {
            // TODO: Gerçek rapor mantığı eklenecek
            return Task.FromResult<object>(new { Success = true, Message = $"Firma {firmaId} gelir gider raporu" });
        }
    }
}
// powered by 1986sec