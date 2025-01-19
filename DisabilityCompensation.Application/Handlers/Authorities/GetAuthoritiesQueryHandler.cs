using DisabilityCompensation.Application.Queries.Authorities;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Authorities
{
    public class GetAuthoritiesQueryHandler : IRequestHandler<GetAuthoritiesQuery, BaseResponse<List<AuthorityDto>>>
    {
        private readonly IAuthorityService _authorityService;

        public GetAuthoritiesQueryHandler(IAuthorityService authorityService)
        {
            _authorityService = authorityService;
        }
        public async Task<BaseResponse<List<AuthorityDto>>> Handle(GetAuthoritiesQuery request, CancellationToken cancellationToken)
        {
            var authorities = await _authorityService.FindAsync<AuthorityDto>(x => x.IsActive);

            return new BaseResponse<List<AuthorityDto>>
            {
                Data = authorities.ToList(),
                Succcess = true
            };
        }
    }
}
