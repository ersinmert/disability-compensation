using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;
using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Shared.Dtos.Enums;
using DisabilityCompensation.Shared.Utilities;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class KnownPeriodCompensationCalculator : BaseCompensationCalculator, ICompensationCalculator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISalaryCalculatorFactory _salaryCalculatorFactory;
        private readonly IDisabilityRateCalculatorFactory _disabilityRateCalculatorFactory;
        private readonly IDateRangeCalculatorFactory _dateRangeCalculatorFactory;
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public KnownPeriodCompensationCalculator(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISalaryCalculatorFactory salaryCalculatorFactory,
            IDisabilityRateCalculatorFactory disabilityRateCalculatorFactory,
            IDateRangeCalculatorFactory dateRangeCalculatorFactory,
            IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _salaryCalculatorFactory = salaryCalculatorFactory;
            _disabilityRateCalculatorFactory = disabilityRateCalculatorFactory;
            _dateRangeCalculatorFactory = dateRangeCalculatorFactory;
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<CompensationCalculatorResultDto> CalculateAsync(Guid compensationId)
        {
            var compensation = await _unitOfWork.CompensationRepository.GetByIdAsync(compensationId);
            var compensationDto = _mapper.Map<CompensationDto>(compensation);

            var knownPeriodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new KnownPeriodDatesSpecifierDto
            {
                Period = Periods.Known
            }).SpecifyAsync(compensationDto!);
            var knownPeriodStartDate = knownPeriodDates.StartDate;
            var knownPeriodEndDate = knownPeriodDates.EndDate;
            var minimumWages = await _unitOfWork.MinimumWageRepository.GetMinimumWagesAsync(knownPeriodStartDate, knownPeriodEndDate);
            var minimumWagesDto = _mapper.Map<List<MinimumWageDto>>(minimumWages);

            HashSet<DateRangeDto> dateRanges = await _dateRangeCalculatorFactory.CreateCalculator(new KnownPeriodDateRangeCalculatorDto
            {
                Period = Periods.Known
            }).GetDateRangesAsync(compensationDto!);

            decimal totalKnownPeriodCompnsationAmount = 0;
            List<CompensationCalculationDto> compensationCalculations = new List<CompensationCalculationDto>();
            foreach (var dateRange in dateRanges)
            {
                var compensationClaimantAmount = await ClaimantCalculate(compensationDto!, minimumWagesDto, dateRange, compensationCalculations);
                totalKnownPeriodCompnsationAmount += compensationClaimantAmount;
                if (compensationDto!.HasCaregiver == true)
                {
                    var compensationCaregiverAmount = await CaregiverCalculate(compensationDto!, minimumWagesDto, dateRange, compensationCalculations);
                    totalKnownPeriodCompnsationAmount += compensationCaregiverAmount;
                }
            }

            return new CompensationCalculatorResultDto
            {
                Amount = totalKnownPeriodCompnsationAmount,
                CompensationCalculations = compensationCalculations
            };
        }

        private async Task<decimal> ClaimantCalculate(CompensationDto compensation, List<MinimumWageDto> minimumWages, DateRangeDto dateRange, List<CompensationCalculationDto> compensationCalculations)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new KnownPeriodClaimantSalaryCalculatorDto
            {
                Period = Periods.Known,
                SalaryCalculatorType = SalaryCalculatorTypes.Claimant,
                Compensation = compensation,
                DateRange = dateRange,
                MinimumWages = minimumWages
            }).Calculate();
            decimal dailySalary = monthlySalary / AppConstants.DaysInMonth;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new KnownPeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Known
            }).CalculateAsync(compensation, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);
            var compensationClaimantAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            compensationCalculations.Add(new CompensationCalculationDto(
                compensation,
                dateRange,
                compensationClaimantAmount,
                disabilityRate,
                Periods.Known,
                monthlySalary,
                totalDays,
                SalaryCalculatorTypes.Claimant)
            );

            return compensationClaimantAmount;
        }

        private async Task<decimal> CaregiverCalculate(CompensationDto compensation, List<MinimumWageDto> minimumWages, DateRangeDto dateRange, List<CompensationCalculationDto> compensationCalculations)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new KnownPeriodCaregiverSalaryCalculatorDto
            {
                Period = Periods.Known,
                SalaryCalculatorType = SalaryCalculatorTypes.Caregiver,
                Compensation = compensation,
                DateRange = dateRange,
                MinimumWages = minimumWages
            }).Calculate();
            decimal dailySalary = monthlySalary / AppConstants.DaysInMonth;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new KnownPeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Known
            }).CalculateAsync(compensation, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationCaregiverAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            compensationCalculations.Add(new CompensationCalculationDto(
                compensation,
                dateRange,
                compensationCaregiverAmount,
                disabilityRate,
                Periods.Known,
                monthlySalary,
                totalDays,
                SalaryCalculatorTypes.Caregiver)
            );
            return compensationCaregiverAmount;
        }
    }
}
