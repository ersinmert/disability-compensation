using DisabilityCompensation.Application.Queries.Users;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Users
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .NotNull()
                .Must(userId => userId != default)
                .WithMessage("Id boş olamaz!");
        }
    }
}
