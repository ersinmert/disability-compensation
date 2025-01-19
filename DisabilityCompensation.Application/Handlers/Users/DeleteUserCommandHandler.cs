using DisabilityCompensation.Application.Commands.Users;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using DisabilityCompensation.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Application.Handlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<bool>>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userClaim = _httpContextAccessor.HttpContext.GetClaims();
            var result = await _userService.DeleteAsync(request.Id, userClaim);

            return new BaseResponse<bool>
            {
                Data = result,
                Succcess = true
            };
        }
    }
}
