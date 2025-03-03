using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.SalaryCalculator
{
    public interface ISalaryCalculatorDto
    {
        Periods Period { get; set; }
        SalaryCalculatorTypes SalaryCalculatorType { get; set; }
    }
}
