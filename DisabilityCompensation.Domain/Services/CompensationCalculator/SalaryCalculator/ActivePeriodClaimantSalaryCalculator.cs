using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class ActivePeriodClaimantSalaryCalculator : ISalaryCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;
        private readonly ActivePeriodClaimantSalaryCalculatorDto _calculatorDto;

        public ActivePeriodClaimantSalaryCalculator(
            IPeriodDatesSpecifierFactory periodDatesSpecifierFactory,
            ActivePeriodClaimantSalaryCalculatorDto calculatorDto
            )
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
            _calculatorDto = calculatorDto;
        }

        public decimal Calculate()
        {
            decimal salary = 0;
            if (_calculatorDto.Compensation!.Claimant!.IsMinimumWage != true)
            {
                salary = _calculatorDto.Compensation!.Claimant.MonthlyIncome!.Value;
                return salary;
            }

            var activePeriodDates = _periodDatesSpecifierFactory.CreateSpecifier(new ActivePeriodDatesSpecifierDto
            {
                Period = Periods.Active
            }).SpecifyAsync(_calculatorDto.Compensation!);
            salary = _calculatorDto.MinimumWage!.NetWage;

            return salary;
        }
    }
}
