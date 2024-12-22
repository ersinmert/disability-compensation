using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.Application.Queries.Compensations
{
    public class GetCompensationQuery : IRequest<BaseResponse<CompensationDto>>
    {
        [FromRoute(Name = "id")]
        public Guid CompensationId { get; set; }
    }
}
