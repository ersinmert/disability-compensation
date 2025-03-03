using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class KnownPeriodCaregiverSalaryCalculator : ISalaryCalculator
    {
        private readonly KnownPeriodCaregiverSalaryCalculatorDto _calculatorDto;

        public KnownPeriodCaregiverSalaryCalculator(KnownPeriodCaregiverSalaryCalculatorDto calculatorDto)
        {
            _calculatorDto = calculatorDto;
        }

        public decimal Calculate()
        {
            var salary = _calculatorDto.MinimumWages!.FirstOrDefault(x =>
                            x.IsActive
                            &&
                            x.StartDate <= _calculatorDto.DateRange!.StartDate && x.EndDate > _calculatorDto.DateRange!.StartDate
                            &&
                            x.MaritalStatus == _calculatorDto.Compensation!.Claimant!.MaritalStatus
                     )!.GrossWage;

            return salary;
        }
    }
}
