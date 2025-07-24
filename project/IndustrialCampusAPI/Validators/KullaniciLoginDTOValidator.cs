using FluentValidation;
using IndustrialCampusAPI.DTOs;

namespace IndustrialCampusAPI.Validators
{
    public class KullaniciLoginDTOValidator : AbstractValidator<KullaniciLoginDTO>
    {
        public KullaniciLoginDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir.");

            RuleFor(x => x.Sifre)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .MaximumLength(100).WithMessage("Şifre en fazla 100 karakter olabilir.");
        }
    }
} 