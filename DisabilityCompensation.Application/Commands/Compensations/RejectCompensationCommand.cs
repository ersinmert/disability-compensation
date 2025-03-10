using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class RejectCompensationCommand : IRequest<BaseResponse<bool>>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        public string? RejectReason { get; set; }

        [SwaggerIgnore]
        public UserClaim? UserClaim { get; set; }
    }
}
