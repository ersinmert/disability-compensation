using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DisabilityRateCalculator
{
    public class ActivePeriodDisabilityRateCalculator : IDisabilityRateCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public ActivePeriodDisabilityRateCalculator(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<decimal> CalculateAsync(CompensationDto compensation, DateRangeDto dateRange)
        {
            var activePeriodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new ActivePeriodDatesSpecifierDto
            {
                Period = Periods.Active
            }).SpecifyAsync(compensation);
            var activePeriodStartDate = activePeriodDates.StartDate;
            var activePeriodEndDate = activePeriodDates.EndDate;

            var knownPeriodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new KnownPeriodDatesSpecifierDto
            {
                Period = Periods.Known
            }).SpecifyAsync(compensation);
            var knownPeriodStartDate = knownPeriodDates.StartDate;

            if (compensation.HasTemporaryDisability != true)
            {
                return compensation.Event!.DisabilityRate!.Value;
            }

            var temporaryDisabilityEndDate = knownPeriodStartDate.AddDays(compensation!.TemporaryDisabilityDay!.Value);
            var hasMatchedDateRange = dateRange.StartDate < temporaryDisabilityEndDate && dateRange.StartDate >= activePeriodStartDate;
            if (temporaryDisabilityEndDate > activePeriodStartDate && hasMatchedDateRange)
            {
                return AppConstants.TemporaryDisabilityRate;
            }

            return compensation.Event!.DisabilityRate!.Value;
        }
    }
}
