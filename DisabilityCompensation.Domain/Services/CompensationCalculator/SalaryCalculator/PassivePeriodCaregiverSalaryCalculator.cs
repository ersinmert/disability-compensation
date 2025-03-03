using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class PassivePeriodCaregiverSalaryCalculator : ISalaryCalculator
    {
        private readonly PassivePeriodCaregiverSalaryCalculatorDto _calculatorDto;

        public PassivePeriodCaregiverSalaryCalculator(PassivePeriodCaregiverSalaryCalculatorDto calculatorDto)
        {
            _calculatorDto = calculatorDto;
        }

        public decimal Calculate()
        {
            return _calculatorDto.MinimumWage!.GrossWage;
        }
    }
}
