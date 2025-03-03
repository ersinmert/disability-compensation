using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class ApproveCompensationCommandHandler : IRequestHandler<ApproveCompensationCommand, BaseResponse<bool>>
    {
        private readonly ICompensationService _compensationService;
        private readonly IMapper _mapper;
        private readonly ICompensationCalculationManager _compensationCalculationManager;

        public ApproveCompensationCommandHandler(ICompensationService compensationService, IMapper mapper, ICompensationCalculationManager compensationCalculationManager)
        {
            _compensationService = compensationService;
            _mapper = mapper;
            _compensationCalculationManager = compensationCalculationManager;
        }

        public async Task<BaseResponse<bool>> Handle(ApproveCompensationCommand request, CancellationToken cancellationToken)
        {
            var approveDto = _mapper.Map<ApproveCompensationDto>(request);
            var result = await _compensationService.ApproveAsync(approveDto, request.UserClaim!);
            await _compensationService.CalculateAsync(request.Id);
            return new BaseResponse<bool> { Data = result };
        }
    }
}
