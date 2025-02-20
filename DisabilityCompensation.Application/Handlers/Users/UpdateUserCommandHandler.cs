using AutoMapper;
using DisabilityCompensation.Application.Commands.Users;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using DisabilityCompensation.Shared.Extensions;

namespace DisabilityCompensation.Application.Handlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateUserCommandHandler(
            IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<BaseResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updateDto = _mapper.Map<UpdateUserDto>(request);
            var userDto = await _userService.UpdateAsync(updateDto, request.UserClaim!);

            return new BaseResponse<UserDto>
            {
                Data = userDto,
                Succcess = true
            };
        }
    }
}
