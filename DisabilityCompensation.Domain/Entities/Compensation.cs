using DisabilityCompensation.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("Compensation")]
    public class Compensation : BaseEntity
    {
        public string? PredefinedNote { get; set; }
        public string? Note { get; set; }
        public CompensationStatus Status { get; set; } = CompensationStatus.Pending;
        public bool? HasTemporaryDisability { get; set; }
        public bool? HasCaregiver { get; set; }
        public int? TemporaryDisabilityDay { get; set; }
        public string? RejectReason { get; set; }
        public decimal? TotalAmount { get; set; }

        #region Relations

        public required Claimant Claimant { get; set; }
        public required Event Event { get; set; }
        public List<Expense>? Expenses { get; set; }
        public List<Document>? Documents { get; set; }
        public User? CreatedByUser { get; set; }
        public List<CompensationCalculation>? CompensationCalculation { get; set; }

        #endregion
    }
}
