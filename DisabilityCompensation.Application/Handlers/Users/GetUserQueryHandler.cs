using DisabilityCompensation.Application.Queries.Users;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<UserDto>>
    {
        private readonly IUserService _userService;

        public GetUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FirstOrDefaultAsync<UserDto>(x => x.Id == request.Id);

            return new BaseResponse<UserDto>
            {
                Data = user,
                Succcess = true
            };
        }
    }
}
