using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class PassivePeriodClaimantSalaryCalculator : ISalaryCalculator
    {
        private readonly PassivePeriodClaimantSalaryCalculatorDto _calculatorDto;

        public PassivePeriodClaimantSalaryCalculator(PassivePeriodClaimantSalaryCalculatorDto calculatorDto)
        {
            _calculatorDto = calculatorDto;
        }

        public decimal Calculate()
        {
            return _calculatorDto.MinimumWage!.NetWage;
        }
    }
}
