using DisabilityCompensation.Application.Commands.Compensations;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Compensations
{
    public class ApproveCompensationCommandValidator : AbstractValidator<ApproveCompensationCommand>
    {
        public ApproveCompensationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().Must(x => x != default).WithMessage("Geçerli bir Id giriniz!");

            RuleFor(x => x.DisabilityRate)
                .GreaterThan(0).WithMessage("Maluliyet oranı sıfırdan büyük olmalıdır!");

            RuleFor(x => x.TemporaryDisabilityDay)
                .GreaterThan(0).WithMessage("Geçici iş göremezlik süresi sıfırdan büyük olmalıdır!");
        }
    }
}
