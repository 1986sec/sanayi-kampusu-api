// powered by 1986sec
namespace IndustrialCampusAPI.DTOs
{
    public class KullaniciDTO
    {
        public int KullaniciID { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public DateTime OlusturmaTarihi { get; set; }
        public bool Aktif { get; set; }
    }

    public class KullaniciRegisterDTO
    {
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public string Rol { get; set; } = "Personel";
    }

    public class KullaniciLoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
    }

    public class KullaniciUpdateDTO
    {
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "Personel";
        public bool Aktif { get; set; } = true;
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public KullaniciDTO Kullanici { get; set; } = null!;
    }

    public class SifreSifirlamaTalepDTO
    {
        public string Email { get; set; } = string.Empty;
    }

    public class SifreSifirlamaOnayDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string YeniSifre { get; set; } = string.Empty;
    }

    public class SifreDegistirDTO
    {
        public int KullaniciID { get; set; }
        public string MevcutSifre { get; set; } = string.Empty;
        public string YeniSifre { get; set; } = string.Empty;
    }

    public class RefreshTokenRequestDTO
    {
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public KullaniciDTO Kullanici { get; set; } = null!;
    }
}
// powered by 1986sec