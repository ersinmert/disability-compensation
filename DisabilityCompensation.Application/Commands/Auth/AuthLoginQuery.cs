using DisabilityCompensation.Application.Dtos.Auth;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Commands.Auth
{
    public class AuthLoginQuery : IRequest<BaseResponse<LoginResponseDto>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
