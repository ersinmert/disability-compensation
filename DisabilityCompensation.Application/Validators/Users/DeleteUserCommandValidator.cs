using DisabilityCompensation.Application.Commands.Users;
using FluentValidation;

namespace DisabilityCompensation.Application.Validators.Users
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .NotNull()
                .Must(id => id != default)
                .WithMessage("id boş veya varsayılan değer olamaz.");
        }
    }
}
