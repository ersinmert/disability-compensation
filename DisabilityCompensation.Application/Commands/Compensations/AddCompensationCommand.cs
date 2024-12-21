using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class AddCompensationCommand : IRequest<BaseResponse<Guid>>
    {
        public CompensationDto? Compensation { get; set; }
    }
}
