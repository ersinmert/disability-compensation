using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;
using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;
using DisabilityCompensation.Shared.Utilities;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class KnownPeriodCompensationCalculator : BaseCompensationCalculator, ICompensationCalculator
    {
        private readonly IMinimumWageService _minimumWageService;
        private readonly ICompensationService _compensationService;
        private readonly ISalaryCalculatorFactory _salaryCalculatorFactory;
        private readonly IDisabilityRateCalculatorFactory _disabilityRateCalculatorFactory;
        private readonly IDateRangeCalculatorFactory _dateRangeCalculatorFactory;
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public KnownPeriodCompensationCalculator(
            IMinimumWageService minimumWageService,
            ICompensationService compensationService,
            ISalaryCalculatorFactory salaryCalculatorFactory,
            IDisabilityRateCalculatorFactory disabilityRateCalculatorFactory,
            IDateRangeCalculatorFactory dateRangeCalculatorFactory,
            IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _minimumWageService = minimumWageService;
            _compensationService = compensationService;
            _salaryCalculatorFactory = salaryCalculatorFactory;
            _disabilityRateCalculatorFactory = disabilityRateCalculatorFactory;
            _dateRangeCalculatorFactory = dateRangeCalculatorFactory;
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<decimal> CalculateAsync(Guid compensationId)
        {
            var compensation = await _compensationService.GetByIdAsync<CompensationDto>(compensationId);

            var knownPeriodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new KnownPeriodDatesSpecifierDto
            {
                Period = Periods.Known
            }).SpecifyAsync(compensation!);
            var knownPeriodStartDate = knownPeriodDates.StartDate;
            var knownPeriodEndDate = knownPeriodDates.EndDate;
            var minimumWages = await _minimumWageService.GetMinimumWagesAsync(knownPeriodStartDate, knownPeriodEndDate);

            HashSet<DateRangeDto> dateRanges = await _dateRangeCalculatorFactory.CreateCalculator(new KnownPeriodDateRangeCalculatorDto
            {
                Period = Periods.Known
            }).GetDateRangesAsync(compensation!);

            decimal totalKnownPeriodCompnsationAmount = 0;
            foreach (var dateRange in dateRanges)
            {
                var compensationClaimantAmount = await ClaimantCalculate(compensation!, minimumWages, dateRange);
                totalKnownPeriodCompnsationAmount += compensationClaimantAmount;
                if (compensation!.HasCaregiver == true)
                {
                    var compensationCaregiverAmount = await CaregiverCalculate(compensation!, minimumWages, dateRange);
                    totalKnownPeriodCompnsationAmount += compensationCaregiverAmount;
                }
            }

            return totalKnownPeriodCompnsationAmount;
        }

        private async Task<decimal> ClaimantCalculate(CompensationDto compensation, List<MinimumWageDto> minimumWages, DateRangeDto dateRange)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new KnownPeriodClaimantSalaryCalculatorDto
            {
                Period = Periods.Known,
                SalaryCalculatorType = SalaryCalculatorTypes.Claimant,
                Compensation = compensation,
                DateRange = dateRange,
                MinimumWages = minimumWages
            }).Calculate();
            decimal dailySalary = monthlySalary / 30;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new KnownPeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Known
            }).CalculateAsync(compensation, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationClaimantAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            return compensationClaimantAmount;
        }

        private async Task<decimal> CaregiverCalculate(CompensationDto compensation, List<MinimumWageDto> minimumWages, DateRangeDto dateRange)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new KnownPeriodCaregiverSalaryCalculatorDto
            {
                Period = Periods.Known,
                SalaryCalculatorType = SalaryCalculatorTypes.Caregiver,
                Compensation = compensation,
                DateRange = dateRange,
                MinimumWages = minimumWages
            }).Calculate();
            decimal dailySalary = monthlySalary / 30;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new KnownPeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Known
            }).CalculateAsync(compensation, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationCaregiverAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            return compensationCaregiverAmount;
        }
    }
}
