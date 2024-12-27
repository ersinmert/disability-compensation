using DisabilityCompensation.Application.Commands.Auth;
using DisabilityCompensation.Application.Dtos.Auth;
using DisabilityCompensation.Application.Exceptions;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Auth
{
    public class AuthLoginQueryHandler : IRequestHandler<AuthLoginQuery, BaseResponse<LoginResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthLoginQueryHandler(
            IAuthService authService,
            ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        public async Task<BaseResponse<LoginResponseDto>> Handle(AuthLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _authService.Login(request.Email!, request.Password!);
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı veya şifre hatalı!");
            }
            var token = _tokenService.GenerateToken(user!.Id);

            return new BaseResponse<LoginResponseDto>
            {
                Data = new LoginResponseDto
                {
                    Token = token,
                    User = user
                },
                Succcess = true
            };
        }
    }
}
