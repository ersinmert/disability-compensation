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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserCommandHandler(
            IMapper mapper,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userClaim = _httpContextAccessor.HttpContext.GetClaims();
            var updateDto = _mapper.Map<UpdateUserDto>(request);
            var userDto = await _userService.UpdateAsync(updateDto, userClaim);

            return new BaseResponse<UserDto>
            {
                Data = userDto,
                Succcess = true
            };
        }
    }
}
