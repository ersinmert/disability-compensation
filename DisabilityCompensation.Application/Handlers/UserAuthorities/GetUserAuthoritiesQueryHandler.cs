using DisabilityCompensation.Application.Queries.UserAuthorities;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.UserAuthorities
{
    public class GetUserAuthoritiesQueryHandler : IRequestHandler<GetUserAuthoritiesQuery, BaseResponse<List<string>>>
    {
        private readonly IUserAuthorityService _userAuthorityService;

        public GetUserAuthoritiesQueryHandler(IUserAuthorityService userAuthorityService)
        {
            _userAuthorityService = userAuthorityService;
        }

        public async Task<BaseResponse<List<string>>> Handle(GetUserAuthoritiesQuery request, CancellationToken cancellationToken)
        {
            var authorities = await _userAuthorityService.GetAuthoritiesAsync(request.UserId);

            return new BaseResponse<List<string>>
            {
                Data = authorities,
                Succcess = true
            };
        }
    }
}
