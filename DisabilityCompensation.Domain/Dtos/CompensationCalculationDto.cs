using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Shared.Dtos.Bases;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos
{
    public class CompensationCalculationDto : BaseDto
    {
        public CompensationCalculationDto()
        {
        }

        public CompensationCalculationDto(
            CompensationDto compensation,
            DateRangeDto dateRange,
            decimal amount,
            decimal disabilityRate,
            Periods period,
            decimal monthlySalary,
            int totalDays,
            SalaryCalculatorTypes calculatorType)
        {
            CompensationId = compensation.Id;
            StartDate = dateRange.StartDate;
            EndDate = dateRange.EndDate;
            CreatedBy = compensation.CreatedBy;
            Amount = amount;
            DisabilityRate = disabilityRate;
            Wage = monthlySalary;
            Age = compensation.GetAgeOnDate(dateRange.StartDate);
            CalculatorType = calculatorType;
            Period = period;
            MaritalStatus = compensation.Claimant!.MaritalStatus;
            Gender = compensation.Claimant.Gender;
            FaultRate = compensation.Event!.FaultRate!.Value;
            TotalDays = totalDays;
            NumberOfChilren = compensation.Claimant.NumberOfChildren;
        }

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
    }
}
