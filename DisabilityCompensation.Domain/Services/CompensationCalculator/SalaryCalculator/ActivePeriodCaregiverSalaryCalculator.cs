using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class ActivePeriodCaregiverSalaryCalculator : ISalaryCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;
        private readonly ActivePeriodCaregiverSalaryCalculatorDto _calculatorDto;

        public ActivePeriodCaregiverSalaryCalculator(
            IPeriodDatesSpecifierFactory periodDatesSpecifierFactory,
            ActivePeriodCaregiverSalaryCalculatorDto calculatorDto)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
            _calculatorDto = calculatorDto;
        }

        public decimal Calculate()
        {
            var activePeriodDates = _periodDatesSpecifierFactory.CreateSpecifier(new ActivePeriodDatesSpecifierDto
            {
                Period = Periods.Active
            }).SpecifyAsync(_calculatorDto.Compensation!);
            var salary = _calculatorDto.MinimumWage!.GrossWage;

            return salary;
        }
    }
}
