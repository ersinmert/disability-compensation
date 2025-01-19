using DisabilityCompensation.Application.Commands.Users;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .Must(id => id != default)
                    .WithMessage("id boş veya varsayılan değer olamaz.");
            RuleFor(user => user.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("İsim boş olamaz.");
            RuleFor(user => user.Surname)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Soyisim boş olamaz.");
            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Email boş olamaz.")
                .EmailAddress()
                    .WithMessage("Geçerli bir email adresi giriniz.");
            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Şifre boş olamaz.");
        }
    }
}
