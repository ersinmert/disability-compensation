using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Shared.Dtos
{
    public class CompensationDto : BaseDto
    {
        public string? PredefinedNote { get; set; }
        public string? Note { get; set; }

        public ClaimantDto? Claimant { get; set; }
        public EventDto? Event { get; set; }

        public List<ExpenseDto>? Expenses { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }
}
