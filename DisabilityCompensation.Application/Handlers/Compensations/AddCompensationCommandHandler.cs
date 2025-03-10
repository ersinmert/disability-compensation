using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class AddCompensationCommandHandler : IRequestHandler<AddCompensationCommand, BaseResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly ICompensationService _compensationService;

        public AddCompensationCommandHandler(
            ICompensationService compensationService,
            IMapper mapper)
        {
            _compensationService = compensationService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Guid>> Handle(AddCompensationCommand request, CancellationToken cancellationToken)
        {
            var compensation = _mapper.Map<CompensationDto>(request);
            compensation.Status = Domain.ValueObjects.CompensationStatus.Pending;
            var id = await _compensationService.AddAsync(compensation, request.UserClaim!);

            return new BaseResponse<Guid>
            {
                Data = id,
                Succcess = true
            };
        }
    }
}
