using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("CompensationCalculation")]
    public class CompensationCalculation : BaseEntity
    {
        public Guid CompensationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal DisabilityRate { get; set; }
        public decimal FaultRate { get; set; }
        public decimal Amount { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public int TotalDays { get; set; }
        public decimal Wage { get; set; }
        public int? NumberOfChilren { get; set; }
        public Periods Period { get; set; }
        public SalaryCalculatorTypes CalculatorType { get; set; }
        public string? MaritalStatus { get; set; }

        #region Relations

        public Compensation? Compensation { get; set; }

        #endregion
    }
}
