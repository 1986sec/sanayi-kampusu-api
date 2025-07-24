# powered by 1986sec

# Sanayi Kampüsü Dijital Yönetim Sistemi - Backend API

Merhaba! Bu proje, sanayi kampüslerinde firma, ziyaret, gelir-gider ve kullanıcı yönetimini kolaylaştırmak için ASP.NET Core ile geliştirilmiş bir web API uygulamasıdır. Kodlar tamamen bana aittir ve izinsiz kullanılamaz.

---

## Temel Özellikler

- **Firma Yönetimi:** Firmaları ekle, güncelle, sil, listele. Arama ve filtreleme desteği.
- **Kullanıcı Yönetimi:** Kayıt, giriş, rol bazlı yetkilendirme (Admin/Personel), kullanıcı güncelleme ve silme.
- **Ziyaret Takibi:** Ziyaret kayıtlarını oluştur, güncelle, sil ve raporla. Firma ve kullanıcı bazlı filtreleme.
- **Gelir-Gider Takibi:** Finansal işlemleri kaydet, güncelle, sil. Kar-zarar hesaplama ve raporlama.
- **Raporlama:** PDF ve Excel formatında gelir-gider ve ziyaret raporları al.
- **Güvenlik:** JWT ile kimlik doğrulama, şifreler BCrypt ile hashlenir, CORS ve input validation aktif.

---

## Kullanım Talimatı

1. **Projeyi Çalıştırmak için:**
   - Bilgisayarında .NET 8 SDK kurulu olmalı.
   - Terminalde proje klasörüne gir:
     ```sh
     dotnet restore
     dotnet run --project project/IndustrialCampusAPI.csproj
     ```
   - API varsayılan olarak `http://localhost:5000` adresinde çalışır.

2. **Swagger Arayüzü:**
   - Tarayıcıdan `http://localhost:5000` adresine git.
   - Tüm endpoint’leri ve örnek istekleri burada test edebilirsin.

3. **Varsayılan Admin Kullanıcı:**
   - Email: `admin@campus.com`
   - Şifre: `admin123`

4. **Giriş Yaptıktan Sonra:**
   - Dönen JWT token’ı, diğer isteklerde `Authorization: Bearer {token}` şeklinde header’a ekle.

---

## API Endpoint’leri (Kısa Liste)

- **/api/auth/login** : Kullanıcı girişi
- **/api/auth/register** : Yeni kullanıcı kaydı
- **/api/firma** : Firma işlemleri (GET, POST, PUT, DELETE)
- **/api/ziyaret** : Ziyaret işlemleri (GET, POST, PUT, DELETE)
- **/api/gelirgider** : Gelir-gider işlemleri (GET, POST, PUT, DELETE)
- **/api/rapor/** : PDF/Excel raporlar

Tüm endpoint’ler Swagger’da detaylı açıklanmıştır.

---

## Teknik Detaylar

- **Veritabanı:** SQLite (ilk çalıştırmada otomatik oluşur)
- **ORM:** Entity Framework Core
- **Mapping:** AutoMapper
- **Loglama:** Serilog ile hem konsola hem dosyaya log tutulur (`logs/` klasörü)
- **Şifreleme:** BCrypt.Net ile güvenli şifre saklama
- **Dökümantasyon:** Swagger UI
- **Lisans:** Kodlarım lisanslıdır, izinsiz kullanılamaz. Satın almadan veya izin almadan kullanmak yasaktır.

---

## Geliştirici Notları

- Kodlar sade, okunabilir ve profesyonel standartlara uygun şekilde yazılmıştır.
- Herhangi bir sorunda veya geliştirme talebinde bana ulaşabilirsin.
- Projeyi frontend ile entegre etmek için API adresini frontend tarafında `.env` veya config dosyasına eklemen yeterli.
- CORS ayarlarını kendi domainine göre güncelleyebilirsin.

---

## Hızlı Başlangıç

1. .NET 8 SDK’yı indir ve kur.
2. Proje klasöründe terminal aç:
   ```sh
   dotnet restore
   dotnet run --project project/IndustrialCampusAPI.csproj
   ```
3. Tarayıcıdan `http://localhost:5000` adresine git.
4. Swagger’dan giriş yap ve API’yi keşfet.

---

## İletişim & Lisans

Bu yazılım tescillidir. Satın almadan veya 1986sec’ten yazılı izin almadan kullanılamaz. Tüm hakları saklıdır.

Soruların için: [1986sec](mailto:seninmailin@domain.com)

# powered by 1986sec