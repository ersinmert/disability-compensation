using AutoMapper;
using DisabilityCompensation.Application.Commands.Users;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using DisabilityCompensation.Shared.Extensions;
using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Application.Handlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, BaseResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddUserCommandHandler(
            IMapper mapper,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<Guid>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userClaim = _httpContextAccessor.HttpContext.GetClaims();
            var user = _mapper.Map<UserDto>(request);
            var id = await _userService.AddAsync(user, userClaim);

            return new BaseResponse<Guid>
            {
                Data = id,
                Succcess = true
            };
        }
    }
}
