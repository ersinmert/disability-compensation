using DisabilityCompensation.Domain.Dtos.SalaryCalculator;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator
{
    public interface ISalaryCalculatorFactory
    {
        ISalaryCalculator CreateCalculator(ISalaryCalculatorDto salaryCalculatorDto);
    }
}
