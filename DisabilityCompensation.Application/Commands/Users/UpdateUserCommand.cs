using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace DisabilityCompensation.Application.Commands.Users
{
    public class UpdateUserCommand : IRequest<BaseResponse<UserDto>>
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }

        [SwaggerIgnore]
        public UserClaim? UserClaim { get; set; }
    }
}
