using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class RejectCompensationCommandHandler : IRequestHandler<RejectCompensationCommand, BaseResponse<bool>>
    {
        private readonly ICompensationService _compensationService;
        private readonly IMapper _mapper;

        public RejectCompensationCommandHandler(ICompensationService compensationService, IMapper mapper)
        {
            _compensationService = compensationService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<bool>> Handle(RejectCompensationCommand request, CancellationToken cancellationToken)
        {
            var rejectDto = _mapper.Map<RejectCompensationDto>(request);
            var result = await _compensationService.RejectAsync(rejectDto, request.UserClaim!);
            return new BaseResponse<bool> { Data = result };
        }
    }
}
