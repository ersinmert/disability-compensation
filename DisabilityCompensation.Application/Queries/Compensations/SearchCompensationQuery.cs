using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.ValueObjects;
using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Compensations
{
    public class SearchCompensationQuery : IRequest<BaseResponse<PagedResultDto<CompensationDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public CompensationStatus? Status { get; set; }
        public DateTime? Date { get; set; }

        public UserClaim? UserClaim { get; set; }
    }
}
