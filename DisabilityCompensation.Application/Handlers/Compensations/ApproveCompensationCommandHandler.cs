using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class ApproveCompensationCommandHandler : IRequestHandler<ApproveCompensationCommand, BaseResponse<bool>>
    {
        private readonly ICompensationService _compensationService;
        private readonly IMapper _mapper;

        public ApproveCompensationCommandHandler(ICompensationService compensationService, IMapper mapper)
        {
            _compensationService = compensationService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(ApproveCompensationCommand request, CancellationToken cancellationToken)
        {
            var approveDto = _mapper.Map<ApproveCompensationDto>(request);
            var result = await _compensationService.ApproveAsync(approveDto, request.UserClaim!);
            return new BaseResponse<bool> { Data = result };
        }
    }
}
