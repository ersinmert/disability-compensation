using DisabilityCompensation.Application.Dtos;
using MediatR;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class AddCompensationCommand : IRequest<string>
    {
        public AddCompensationRequest? CompensationRequest { get; set; }
    }
}
