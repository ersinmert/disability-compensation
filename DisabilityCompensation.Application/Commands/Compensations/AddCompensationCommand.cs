using DisabilityCompensation.Application.Dtos.Compensation.AddCompensation;
using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class AddCompensationCommand : IRequest<BaseResponse<Guid>>
    {
        public string? PredefinedNote { get; set; }
        public string? Note { get; set; }
        public ClaimantRequest? Claimant { get; set; }
        public EventRequest? Event { get; set; }
        public List<ExpenseRequest>? Expenses { get; set; }
        public List<DocumentRequest>? Documents { get; set; }

        public UserClaim? UserClaim { get; set; }
    }
}
