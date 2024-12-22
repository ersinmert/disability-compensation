using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Application.Queries.Compensations;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class GetCompensationQueryHandler : IRequestHandler<GetCompensationQuery, BaseResponse<CompensationDto>>
    {
        private readonly ICompensationService _compensationService;
        private readonly IMapper _mapper;

        public GetCompensationQueryHandler(
            ICompensationService compensationService,
            IMapper mapper)
        {
            _mapper = mapper;
            _compensationService = compensationService;
        }

        public async Task<BaseResponse<CompensationDto>> Handle(GetCompensationQuery request, CancellationToken cancellationToken)
        {
            var id = request.CompensationId;
            var compensation = await _compensationService.GetByIdAsync<CompensationDto>(id);

            return new BaseResponse<CompensationDto>
            {
                Data = compensation,
                Succcess = true
            };
        }
    }
}
