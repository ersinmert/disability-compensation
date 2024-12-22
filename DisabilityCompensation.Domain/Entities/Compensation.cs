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

        #region Relations

        public required Claimant Claimant { get; set; }
        public required Event Event { get; set; }
        public List<Expense>? Expenses { get; set; }
        public List<Document>? Documents { get; set; }

        #endregion
    }
}
