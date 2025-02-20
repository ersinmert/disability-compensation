using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Application.Queries.Compensations;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class SearchCompensationQueryHandler : IRequestHandler<SearchCompensationQuery, BaseResponse<PagedResultDto<CompensationDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICompensationService _compensationService;

        public SearchCompensationQueryHandler(
            ICompensationService compensationService,
            IMapper mapper)
        {
            _mapper = mapper;
            _compensationService = compensationService;
        }

        public async Task<BaseResponse<PagedResultDto<CompensationDto>>> Handle(SearchCompensationQuery request, CancellationToken cancellationToken)
        {
            var search = _mapper.Map<SearchCompensationDto>(request);
            var compensations = await _compensationService.SearchPagedAsync(search, request.UserClaim!);

            return new BaseResponse<PagedResultDto<CompensationDto>>
            {
                Data = compensations,
                Succcess = true
            };
        }
    }
}
