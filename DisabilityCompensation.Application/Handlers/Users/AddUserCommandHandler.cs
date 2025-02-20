using AutoMapper;
using DisabilityCompensation.Application.Commands.Users;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Application.Handlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, BaseResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AddUserCommandHandler(
            IMapper mapper,
            IUserService userService)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Guid>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserDto>(request);
            var id = await _userService.AddAsync(user, request.UserClaim!);

            return new BaseResponse<Guid>
            {
                Data = id,
                Succcess = true
            };
        }
    }
}
