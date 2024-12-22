using DisabilityCompensation.Application.Queries.Compensations;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Compensations
{
    public class GetCompensationQueryValidator : AbstractValidator<GetCompensationQuery>
    {
        public GetCompensationQueryValidator()
        {
            RuleFor(x => x.CompensationId).NotNull().NotEmpty().WithMessage("CompensationId boş olamaz");
        }
    }
}
