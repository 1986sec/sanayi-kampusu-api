# Industrial Campus API - Sanayi Kampüsü Dijital Yönetim Sistemi

Bu proje, ASP.NET Core tabanlı, rol bazlı yetkilendirmeye sahip, RESTful API endpoint'leriyle frontend'e veri sağlayan bir Sanayi Kampüsü Dijital Yönetim Sistemi'dir.

## 🚀 Özellikler

### Temel Modüller
- **Firma Yönetimi**: CRUD operasyonları, arama, filtreleme ve sıralama
- **Ziyaret Yönetimi**: Kullanıcı-firma ziyaret takibi, tarih bazlı listeleme
- **Gelir-Gider Yönetimi**: Firma bazlı finansal kayıt takibi, kar/zarar hesaplama
- **Kullanıcı Yönetimi**: JWT tabanlı kimlik doğrulama, rol bazlı yetkilendirme
- **Raporlama**: PDF/Excel export işlevleri

### Teknik Özellikler
- **ASP.NET Core 7.0** Web API
- **Entity Framework Core** ile Code-First yaklaşım
- **SQLite** veritabanı (geliştirme ortamı için)
- **JWT Authentication** ve rol bazlı yetkilendirme
- **AutoMapper** ile DTO mapping
- **Swagger UI** ile API dokümantasyonu
- **Serilog** ile loglama
- **Repository Pattern** ve **Service Layer** katmanlı mimari

## 📋 Gereksinimler

- .NET 7.0 SDK
- SQLite (otomatik oluşturulur)

## 🛠️ Kurulum

1. **Projeyi klonlayın:**
```bash
git clone <repository-url>
cd IndustrialCampusAPI
```

2. **Bağımlılıkları yükleyin:**
```bash
dotnet restore
```

3. **Veritabanını oluşturun:**
```bash
dotnet ef database update
```
(Veya uygulama ilk çalıştırıldığında otomatik oluşturulur)

4. **Uygulamayı çalıştırın:**
```bash
dotnet run
```

5. **Swagger UI'a erişin:**
```
https://localhost:5001
```

## 🔐 Kimlik Doğrulama

### Varsayılan Admin Hesabı
- **Email**: admin@campus.com
- **Şifre**: admin123

### API Kullanımı
1. `/api/auth/login` endpoint'ine POST isteği gönderin
2. Dönen JWT token'ı Authorization header'ında kullanın:
```
Authorization: Bearer <your-jwt-token>
```

## 📚 API Endpoint'leri

### Authentication
- `POST /api/auth/login` - Kullanıcı girişi
- `POST /api/auth/register` - Yeni kullanıcı kaydı

### Firma Yönetimi
- `GET /api/firma` - Tüm firmaları listele (arama/filtreleme destekli)
- `GET /api/firma/{id}` - Firma detayı
- `POST /api/firma` - Yeni firma oluştur (Admin)
- `PUT /api/firma/{id}` - Firma güncelle (Admin)
- `DELETE /api/firma/{id}` - Firma sil (Admin)

### Ziyaret Yönetimi
- `GET /api/ziyaret` - Ziyaretleri listele
- `GET /api/ziyaret/{id}` - Ziyaret detayı
- `POST /api/ziyaret` - Yeni ziyaret oluştur
- `PUT /api/ziyaret/{id}` - Ziyaret güncelle
- `DELETE /api/ziyaret/{id}` - Ziyaret sil
- `GET /api/ziyaret/firma/{firmaId}` - Firmaya göre ziyaretler
- `GET /api/ziyaret/tarih` - Tarih aralığına göre ziyaretler

### Gelir-Gider Yönetimi
- `GET /api/gelirgider` - Tüm kayıtları listele
- `GET /api/gelirgider/{id}` - Kayıt detayı
- `POST /api/gelirgider` - Yeni kayıt oluştur (Admin)
- `PUT /api/gelirgider/{id}` - Kayıt güncelle (Admin)
- `DELETE /api/gelirgider/{id}` - Kayıt sil (Admin)
- `GET /api/gelirgider/firma/{firmaId}` - Firmaya göre kayıtlar
- `GET /api/gelirgider/karzarar/{firmaId}` - Kar/zarar raporu

### Raporlama
- `GET /api/rapor/ziyaretler/{firmaId}/pdf` - Ziyaret raporu PDF
- `GET /api/rapor/ziyaretler/{firmaId}/excel` - Ziyaret raporu Excel
- `GET /api/rapor/gelirgider/{firmaId}/pdf` - Gelir/gider raporu PDF
- `GET /api/rapor/gelirgider/{firmaId}/excel` - Gelir/gider raporu Excel

## 🏗️ Proje Yapısı

```
IndustrialCampusAPI/
├── Controllers/          # API Controller'ları
├── Data/                # DbContext ve veritabanı konfigürasyonu
├── DTOs/                # Data Transfer Objects
├── Mappings/            # AutoMapper profilleri
├── Models/              # Entity modelleri
├── Repositories/        # Repository pattern implementasyonu
├── Services/            # Business logic katmanı
├── appsettings.json     # Uygulama konfigürasyonu
└── Program.cs           # Uygulama başlangıç noktası
```

## 🔒 Rol Bazlı Yetkilendirme

### Admin Rolleri
- Tüm endpoint'lere erişim
- Firma CRUD operasyonları
- Gelir/gider CRUD operasyonları
- Tüm ziyaretleri görüntüleme

### Personel Rolleri
- Sadece kendi ziyaretlerini görüntüleme/düzenleme
- Firma listesini görüntüleme
- Ziyaret oluşturma

## 📊 Veritabanı Şeması

### Tablolar
- **Firmalar**: Firma bilgileri
- **Kullanicilar**: Kullanıcı hesapları ve roller
- **Ziyaretler**: Firma ziyaret kayıtları
- **GelirGiderler**: Finansal işlem kayıtları

### İlişkiler
- Firma → Ziyaretler (1-N)
- Firma → GelirGiderler (1-N)
- Kullanici → Ziyaretler (1-N)

## 🔧 Konfigürasyon

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=industrialcampus.db"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "IndustrialCampusAPI",
    "Audience": "IndustrialCampusAPI"
  }
}
```

## 📝 Loglama

Uygulama Serilog kullanarak:
- Console'a log yazma
- `logs/` klasörüne günlük dosyalar oluşturma
- HTTP request/response loglama

## 🧪 Test Etme

1. Swagger UI kullanarak endpoint'leri test edin
2. Postman collection'ı oluşturun
3. Unit testler için xUnit framework'ü kullanabilirsiniz

## 🚀 Production Deployment

1. **Connection String'i güncelleyin** (SQL Server için)
2. **JWT Key'i güvenli bir değerle değiştirin**
3. **HTTPS konfigürasyonu yapın**
4. **Environment variables kullanın**

## 📞 Destek

05421519782
github.com/1986sec
1986sec.xyz

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.