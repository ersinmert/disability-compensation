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

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.DeleteAsync(request.Id, request.UserClaim!);

            return new BaseResponse<bool>
            {
                Data = result,
                Succcess = true
            };
        }
    }
}
