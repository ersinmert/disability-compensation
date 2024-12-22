using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Application.Queries.Compensations;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class GetAllCompensationQueryHandler : IRequestHandler<GetAllCompensationQuery, BaseResponse<IEnumerable<CompensationDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICompensationService _compensationService;

        public GetAllCompensationQueryHandler(
            ICompensationService compensationService,
            IMapper mapper)
        {
            _mapper = mapper;
            _compensationService = compensationService;
        }

        public async Task<BaseResponse<IEnumerable<CompensationDto>>> Handle(GetAllCompensationQuery request, CancellationToken cancellationToken)
        {
            var compensations = await _compensationService.FindAsync(x => x.IsActive);
            var compensationDtos = _mapper.Map<List<CompensationDto>>(compensations);

            return new BaseResponse<IEnumerable<CompensationDto>>
            {
                Data = compensationDtos,
                Succcess = true
            };
        }
    }
}
