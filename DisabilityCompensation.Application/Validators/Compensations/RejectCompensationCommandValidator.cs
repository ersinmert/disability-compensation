using DisabilityCompensation.Application.Commands.Compensations;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Compensations
{
    public class RejectCompensationCommandValidator : AbstractValidator<RejectCompensationCommand>
    {
        public RejectCompensationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().Must(x => x != default).WithMessage("Geçerli bir Id giriniz!");

            RuleFor(x => x.RejectReason)
                .NotEmpty().NotNull().WithMessage("Red sebebi boş geçilemez!");
        }
    }
}
