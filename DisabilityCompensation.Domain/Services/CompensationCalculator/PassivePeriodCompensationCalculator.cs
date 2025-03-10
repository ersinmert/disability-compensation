using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;
using DisabilityCompensation.Shared.Utilities;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Shared.Constants;
using AutoMapper;
using DisabilityCompensation.Domain.Interfaces;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class PassivePeriodCompensationCalculator : BaseCompensationCalculator, ICompensationCalculator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateRangeCalculatorFactory _dateRangeCalculatorFactory;
        private readonly ISalaryCalculatorFactory _salaryCalculatorFactory;
        private readonly IDisabilityRateCalculatorFactory _disabilityRateCalculatorFactory;

        public PassivePeriodCompensationCalculator(
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
            var compensation = await _unitOfWork.CompensationRepository.GetByIdAsync(compensationId);
            var compensationDto = _mapper.Map<CompensationDto>(compensation);

            var minimumWage = await _unitOfWork.MinimumWageRepository.GetCurrentAsync();
            var mininumWageDto = _mapper.Map<MinimumWageDto>(minimumWage);

            HashSet<DateRangeDto> dateRanges = await _dateRangeCalculatorFactory.CreateCalculator(new PassivePeriodDateRangeCalculatorDto
            {
                Period = Periods.Passive,
            }).GetDateRangesAsync(compensationDto!);

            decimal totalPassivePeriodCompensationAmount = 0;
            List<CompensationCalculationDto> compensationCalculations = new List<CompensationCalculationDto>();
            foreach (var dateRange in dateRanges)
            {
                var compensationClaimantAmount = await ClaimantCalculate(compensationDto, mininumWageDto, dateRange, compensationCalculations);
                totalPassivePeriodCompensationAmount += compensationClaimantAmount;
                if (compensation!.HasCaregiver == true)
                {
                    var compensationCaregiverAmount = await CaregiverCalculate(compensationDto!, mininumWageDto, dateRange, compensationCalculations);
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
            decimal dailySalary = monthlySalary / AppConstants.DaysInMonth;

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
            decimal dailySalary = monthlySalary / AppConstants.DaysInMonth;

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
