using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Commands.Auth
{
    public class AuthLoginQuery : IRequest<BaseResponse<UserDto>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
