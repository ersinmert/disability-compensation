using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Authorities
{
    public class GetAuthoritiesQuery : IRequest<BaseResponse<List<AuthorityDto>>>
    {
    }
}
