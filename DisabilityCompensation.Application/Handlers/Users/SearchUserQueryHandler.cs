using AutoMapper;
using DisabilityCompensation.Application.Queries.Users;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Users
{
    public class SearchUserQueryHandler : IRequestHandler<SearchUserQuery, BaseResponse<PagedResultDto<UserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public SearchUserQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<BaseResponse<PagedResultDto<UserDto>>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
        {
            var search = _mapper.Map<SearchUserDto>(request);
            var users = await _userService.SearchPagedAsync(search);

            return new BaseResponse<PagedResultDto<UserDto>>
            {
                Data = users,
                Succcess = true
            };
        }
    }
}
