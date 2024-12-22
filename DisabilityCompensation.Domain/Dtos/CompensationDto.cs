using DisabilityCompensation.Domain.ValueObjects;
using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Application.Dtos.Entity
{
    public class CompensationDto : BaseDto
    {
        public string? PredefinedNote { get; set; }
        public string? Note { get; set; }
        public CompensationStatus Status { get; set; }

        public ClaimantDto? Claimant { get; set; }
        public EventDto? Event { get; set; }

        public List<ExpenseDto>? Expenses { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }
}
