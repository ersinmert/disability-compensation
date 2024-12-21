using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Domain.Entities;
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
            var compensationDto = request.Compensation;
            var compensation = _mapper.Map<Compensation>(compensationDto);
            await _compensationService.AddAsync(compensation);

            return new BaseResponse<Guid>
            {
                Data = compensation.Id,
                Succcess = true
            };
        }
    }
}
