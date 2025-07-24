using FluentValidation;
using IndustrialCampusAPI.DTOs;

namespace IndustrialCampusAPI.Validators
{
    public class KullaniciRegisterDTOValidator : AbstractValidator<KullaniciRegisterDTO>
    {
        public KullaniciRegisterDTOValidator()
        {
            RuleFor(x => x.AdSoyad)
                .NotEmpty().WithMessage("Ad Soyad zorunludur.")
                .MaximumLength(100).WithMessage("Ad Soyad en fazla 100 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir.");

            RuleFor(x => x.Sifre)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Şifre en fazla 100 karakter olabilir.");

            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("Rol zorunludur.")
                .Must(r => r == "Admin" || r == "Personel")
                .WithMessage("Rol sadece 'Admin' veya 'Personel' olabilir.");
        }
    }
} 