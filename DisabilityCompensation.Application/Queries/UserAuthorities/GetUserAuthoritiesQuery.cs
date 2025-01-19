using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.UserAuthorities
{
    public class GetUserAuthoritiesQuery : IRequest<BaseResponse<List<string>>>
    {
        public Guid UserId { get; set; }
    }
}
