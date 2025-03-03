using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;
using DisabilityCompensation.Shared.Utilities;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class ActivePeriodCompensationCalculator : BaseCompensationCalculator, ICompensationCalculator
    {
        private readonly ICompensationService _compensationService;
        private readonly IMinimumWageService _minimumWageService;
        private readonly IDateRangeCalculatorFactory _dateRangeCalculatorFactory;
        private readonly ISalaryCalculatorFactory _salaryCalculatorFactory;
        private readonly IDisabilityRateCalculatorFactory _disabilityRateCalculatorFactory;

        public ActivePeriodCompensationCalculator(
            ICompensationService compensationService,
            IMinimumWageService minimumWageService,
            IDateRangeCalculatorFactory dateRangeCalculatorFactory,
            ISalaryCalculatorFactory salaryCalculatorFactory,
            IDisabilityRateCalculatorFactory disabilityRateCalculatorFactory)
        {
            _compensationService = compensationService;
            _minimumWageService = minimumWageService;
            _dateRangeCalculatorFactory = dateRangeCalculatorFactory;
            _salaryCalculatorFactory = salaryCalculatorFactory;
            _disabilityRateCalculatorFactory = disabilityRateCalculatorFactory;
        }

        public async Task<decimal> CalculateAsync(Guid compensationId)
        {
            var compensation = await _compensationService.GetByIdAsync<CompensationDto>(compensationId);
            var minimumWage = await _minimumWageService.GetCurrentAsync();

            HashSet<DateRangeDto> dateRanges = await _dateRangeCalculatorFactory.CreateCalculator(new ActivePeriodDateRangeCalculatorDto
            {
                Period = Periods.Active
            }).GetDateRangesAsync(compensation!);

            decimal totalActivePeriodCompnsationAmount = 0;
            foreach (var dateRange in dateRanges)
            {
                var compensationClaimantAmount = await ClaimantCalculate(compensation, minimumWage, dateRange);
                totalActivePeriodCompnsationAmount += compensationClaimantAmount;
                if (compensation!.HasCaregiver == true)
                {
                    var compensationCaregiverAmount = await CaregiverCalculate(compensation!, minimumWage, dateRange);
                    totalActivePeriodCompnsationAmount += compensationCaregiverAmount;
                }
            }

            return totalActivePeriodCompnsationAmount;
        }

        private async Task<decimal> ClaimantCalculate(CompensationDto? compensation, MinimumWageDto minimumWage, DateRangeDto dateRange)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new ActivePeriodClaimantSalaryCalculatorDto
            {
                Period = Periods.Active,
                SalaryCalculatorType = SalaryCalculatorTypes.Claimant,
                Compensation = compensation,
                MinimumWage = minimumWage,
            }).Calculate();
            decimal dailySalary = monthlySalary / 30;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new ActivePeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Active
            }).CalculateAsync(compensation!, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationClaimantAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            return compensationClaimantAmount;
        }

        private async Task<decimal> CaregiverCalculate(CompensationDto? compensation, MinimumWageDto minimumWage, DateRangeDto dateRange)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new ActivePeriodCaregiverSalaryCalculatorDto
            {
                Period = Periods.Active,
                SalaryCalculatorType = SalaryCalculatorTypes.Caregiver,
                Compensation = compensation,
                MinimumWage = minimumWage,
            }).Calculate();
            decimal dailySalary = monthlySalary / 30;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new ActivePeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Active
            }).CalculateAsync(compensation!, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationCaregiverAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            return compensationCaregiverAmount;
        }
    }
}
