using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class RejectCompensationCommand : IRequest<BaseResponse<bool>>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        public string? RejectReason { get; set; }

        public UserClaim? UserClaim { get; set; }
    }
}
