using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Compensations
{
    public class GetCompensationQuery : IRequest<BaseResponse<CompensationDto>>
    {
        public Guid CompensationId { get; set; }
    }
}
