using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;
using DisabilityCompensation.Shared.Utilities;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class PassivePeriodCompensationCalculator : BaseCompensationCalculator, ICompensationCalculator
    {
        private readonly ICompensationService _compensationService;
        private readonly IDateRangeCalculatorFactory _dateRangeCalculatorFactory;
        private readonly IMinimumWageService _minimumWageService;
        private readonly ISalaryCalculatorFactory _salaryCalculatorFactory;
        private readonly IDisabilityRateCalculatorFactory _disabilityRateCalculatorFactory;

        public PassivePeriodCompensationCalculator(
            ICompensationService compensationService,
            IDateRangeCalculatorFactory dateRangeCalculatorFactory,
            IMinimumWageService minimumWageService,
            ISalaryCalculatorFactory salaryCalculatorFactory,
            IDisabilityRateCalculatorFactory disabilityRateCalculatorFactory)
        {
            _compensationService = compensationService;
            _dateRangeCalculatorFactory = dateRangeCalculatorFactory;
            _minimumWageService = minimumWageService;
            _salaryCalculatorFactory = salaryCalculatorFactory;
            _disabilityRateCalculatorFactory = disabilityRateCalculatorFactory;
        }

        public async Task<CompensationCalculatorResultDto> CalculateAsync(Guid compensationId)
        {
            var compensation = await _compensationService.GetByIdAsync<CompensationDto>(compensationId);

            var minimumWage = await _minimumWageService.GetCurrentAsync();

            HashSet<DateRangeDto> dateRanges = await _dateRangeCalculatorFactory.CreateCalculator(new PassivePeriodDateRangeCalculatorDto
            {
                Period = Periods.Passive,
            }).GetDateRangesAsync(compensation!);

            decimal totalPassivePeriodCompensationAmount = 0;
            List<CompensationCalculationDto> compensationCalculations = new List<CompensationCalculationDto>();
            foreach (var dateRange in dateRanges)
            {
                var compensationClaimantAmount = await ClaimantCalculate(compensation, minimumWage, dateRange, compensationCalculations);
                totalPassivePeriodCompensationAmount += compensationClaimantAmount;
                if (compensation!.HasCaregiver == true)
                {
                    var compensationCaregiverAmount = await CaregiverCalculate(compensation!, minimumWage, dateRange, compensationCalculations);
                    totalPassivePeriodCompensationAmount += compensationCaregiverAmount;
                }
            }

            return new CompensationCalculatorResultDto
            {
                Amount = totalPassivePeriodCompensationAmount,
                CompensationCalculations = compensationCalculations
            };
        }

        private async Task<decimal> ClaimantCalculate(CompensationDto? compensation, MinimumWageDto minimumWage, DateRangeDto dateRange, List<CompensationCalculationDto> compensationCalculations)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new PassivePeriodClaimantSalaryCalculatorDto
            {
                Period = Periods.Passive,
                SalaryCalculatorType = SalaryCalculatorTypes.Claimant,
                Compensation = compensation,
                MinimumWage = minimumWage
            }).Calculate();
            decimal dailySalary = monthlySalary / 30;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new PassivePeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Passive
            }).CalculateAsync(compensation!, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationClaimantAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            compensationCalculations.Add(new CompensationCalculationDto(
                compensation,
                dateRange,
                compensationClaimantAmount,
                disabilityRate,
                Periods.Passive,
                monthlySalary,
                totalDays,
                SalaryCalculatorTypes.Claimant)
            );
            return compensationClaimantAmount;
        }

        private async Task<decimal> CaregiverCalculate(CompensationDto? compensation, MinimumWageDto minimumWage, DateRangeDto dateRange, List<CompensationCalculationDto> compensationCalculations)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new PassivePeriodCaregiverSalaryCalculatorDto
            {
                Period = Periods.Passive,
                SalaryCalculatorType = SalaryCalculatorTypes.Caregiver,
                Compensation = compensation,
                MinimumWage = minimumWage,
            }).Calculate();
            decimal dailySalary = monthlySalary / 30;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new PassivePeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Passive
            }).CalculateAsync(compensation!, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationCaregiverAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            compensationCalculations.Add(new CompensationCalculationDto(
                compensation,
                dateRange,
                compensationCaregiverAmount,
                disabilityRate,
                Periods.Passive,
                monthlySalary,
                totalDays,
                SalaryCalculatorTypes.Caregiver)
            );
            return compensationCaregiverAmount;
        }
    }
}
