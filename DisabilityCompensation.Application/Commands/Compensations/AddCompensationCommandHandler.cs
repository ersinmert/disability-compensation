using MediatR;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class AddCompensationCommandHandler : IRequestHandler<AddCompensationCommand, string>
    {
        public AddCompensationCommandHandler()
        {

        }

        public async Task<string> Handle(AddCompensationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
