using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("MinimumWage")]
    public class MinimumWage : BaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal GrossWage { get; set; }
        public decimal NetWage { get; set; }
        public string? MaritalStatus { get; set; }
        public bool? IsUnder16 { get; set; }
        public int? NumberOfChildren { get; set; }
    }
}
