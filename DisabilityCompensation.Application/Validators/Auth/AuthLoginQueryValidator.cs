using DisabilityCompensation.Application.Commands.Auth;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Auth
{
    public class AuthLoginQueryValidator : AbstractValidator<AuthLoginQuery>
    {
        public AuthLoginQueryValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Email formatı geçersiz");

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
