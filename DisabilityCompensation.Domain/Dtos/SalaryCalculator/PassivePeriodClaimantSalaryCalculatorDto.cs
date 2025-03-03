using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.SalaryCalculator
{
    public class PassivePeriodClaimantSalaryCalculatorDto : ISalaryCalculatorDto
    {
        public Periods Period { get; set; }
        public SalaryCalculatorTypes SalaryCalculatorType { get; set; }

        public CompensationDto? Compensation { get; set; }
        public MinimumWageDto? MinimumWage { get; set; }
    }
}
