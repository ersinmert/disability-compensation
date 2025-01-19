using DisabilityCompensation.Application.Queries.UserAuthorities;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.UserAuthorities
{
    public class GetUserAuthoritiesQueryValidator : AbstractValidator<GetUserAuthoritiesQuery>
    {
        public GetUserAuthoritiesQueryValidator()
        {
            RuleFor(request => request.UserId)
                .NotNull()
                .NotEmpty()
                .Must(userId => userId != default)
                    .WithMessage("Id boş olamaz!");
        }
    }
}
