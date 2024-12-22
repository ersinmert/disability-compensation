using DisabilityCompensation.Application.Commands.Auth;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Auth
{
    public class AuthLoginQueryHandler : IRequestHandler<AuthLoginQuery, BaseResponse<UserDto>>
    {
        private readonly IAuthService _authService;

        public AuthLoginQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<BaseResponse<UserDto>> Handle(AuthLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _authService.Login(request.Email!, request.Password!);

            return new BaseResponse<UserDto>
            {
                Data = user,
                Succcess = true
            };
        }
    }
}
