using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Shared.Dtos
{
    public class ExpenseDto : BaseDto
    {
        public Guid CompensationId { get; set; }
        public required string ExpenseType { get; set; }
        public string? ReferenceNo { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Amount { get; set; }
        public string? FilePath { get; set; }
    }
}
