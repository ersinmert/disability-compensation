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
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Domain.Interfaces;
using AutoMapper;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class ActivePeriodCompensationCalculator : BaseCompensationCalculator, ICompensationCalculator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateRangeCalculatorFactory _dateRangeCalculatorFactory;
        private readonly ISalaryCalculatorFactory _salaryCalculatorFactory;
        private readonly IDisabilityRateCalculatorFactory _disabilityRateCalculatorFactory;

        public ActivePeriodCompensationCalculator(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateRangeCalculatorFactory dateRangeCalculatorFactory,
            ISalaryCalculatorFactory salaryCalculatorFactory,
            IDisabilityRateCalculatorFactory disabilityRateCalculatorFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dateRangeCalculatorFactory = dateRangeCalculatorFactory;
            _salaryCalculatorFactory = salaryCalculatorFactory;
            _disabilityRateCalculatorFactory = disabilityRateCalculatorFactory;
        }

        public async Task<CompensationCalculatorResultDto> CalculateAsync(Guid compensationId)
        {
            var compensation = await _unitOfWork.CompensationRepository.GetByIdAsync(compensationId, true);
            var compensationDto = _mapper.Map<CompensationDto>(compensation);
            var minimumWage = await _unitOfWork.MinimumWageRepository.GetCurrentAsync();
            var mininumWageDto = _mapper.Map<MinimumWageDto>(minimumWage);

            HashSet<DateRangeDto> dateRanges = await _dateRangeCalculatorFactory.CreateCalculator(new ActivePeriodDateRangeCalculatorDto
            {
                Period = Periods.Active
            }).GetDateRangesAsync(compensationDto!);

            decimal totalActivePeriodCompnsationAmount = 0;
            List<CompensationCalculationDto> compensationCalculations = new List<CompensationCalculationDto>();
            foreach (var dateRange in dateRanges)
            {
                var compensationClaimantAmount = await ClaimantCalculate(compensationDto, mininumWageDto, dateRange, compensationCalculations);
                totalActivePeriodCompnsationAmount += compensationClaimantAmount;
                if (compensation!.HasCaregiver == true)
                {
                    var compensationCaregiverAmount = await CaregiverCalculate(compensationDto!, mininumWageDto, dateRange, compensationCalculations);
                    totalActivePeriodCompnsationAmount += compensationCaregiverAmount;
                }
            }

            return new CompensationCalculatorResultDto
            {
                Amount = totalActivePeriodCompnsationAmount,
                CompensationCalculations = compensationCalculations
            };
        }

        private async Task<decimal> ClaimantCalculate(CompensationDto? compensation, MinimumWageDto minimumWage, DateRangeDto dateRange, List<CompensationCalculationDto> compensationCalculations)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new ActivePeriodClaimantSalaryCalculatorDto
            {
                Period = Periods.Active,
                SalaryCalculatorType = SalaryCalculatorTypes.Claimant,
                Compensation = compensation,
                MinimumWage = minimumWage,
            }).Calculate();
            decimal dailySalary = monthlySalary / AppConstants.DaysInMonth;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new ActivePeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Active
            }).CalculateAsync(compensation!, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationClaimantAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            compensationCalculations.Add(new CompensationCalculationDto(
                compensation,
                dateRange,
                compensationClaimantAmount,
                disabilityRate,
                Periods.Active,
                monthlySalary,
                totalDays,
                SalaryCalculatorTypes.Claimant)
            );
            return compensationClaimantAmount;
        }

        private async Task<decimal> CaregiverCalculate(CompensationDto? compensation, MinimumWageDto minimumWage, DateRangeDto dateRange, List<CompensationCalculationDto> compensationCalculations)
        {
            var monthlySalary = _salaryCalculatorFactory.CreateCalculator(new ActivePeriodCaregiverSalaryCalculatorDto
            {
                Period = Periods.Active,
                SalaryCalculatorType = SalaryCalculatorTypes.Caregiver,
                Compensation = compensation,
                MinimumWage = minimumWage,
            }).Calculate();
            decimal dailySalary = monthlySalary / AppConstants.DaysInMonth;

            decimal disabilityRate = await _disabilityRateCalculatorFactory.CreateCalculator(new ActivePeriodDisabilityRateCalculatorDto
            {
                Period = Periods.Active
            }).CalculateAsync(compensation!, dateRange);

            int totalDays = DateHelper.BetweenTotalDays(dateRange.StartDate, dateRange.EndDate);

            var compensationCaregiverAmount = CalculateCompensation(dailySalary, totalDays, disabilityRate, compensation!.Event!.FaultRate!.Value);
            compensationCalculations.Add(new CompensationCalculationDto(
                compensation,
                dateRange,
                compensationCaregiverAmount,
                disabilityRate,
                Periods.Active,
                monthlySalary,
                totalDays,
                SalaryCalculatorTypes.Caregiver)
            );
            return compensationCaregiverAmount;
        }
    }
}
