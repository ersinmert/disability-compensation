using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Compensations
{
    public class GetAllCompensationQuery : IRequest<BaseResponse<IEnumerable<CompensationDto>>>
    {

    }
}
