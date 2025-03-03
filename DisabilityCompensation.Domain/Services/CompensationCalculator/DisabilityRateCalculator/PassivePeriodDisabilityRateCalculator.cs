using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DisabilityRateCalculator
{
    public class PassivePeriodDisabilityRateCalculator : IDisabilityRateCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public PassivePeriodDisabilityRateCalculator(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<decimal> CalculateAsync(CompensationDto compensation, DateRangeDto dateRange)
        {
            var passivePeriodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new PassivePeriodDatesSpecifierDto
            {
                Period = Periods.Passive
            }).SpecifyAsync(compensation);

            var passivePeriodStartDate = passivePeriodDates.StartDate;
            var passivePeriodEndDate = passivePeriodDates.EndDate;

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
            var hasMatchedDateRange = dateRange.StartDate < temporaryDisabilityEndDate && dateRange.StartDate >= passivePeriodStartDate;
            if (temporaryDisabilityEndDate > passivePeriodStartDate && hasMatchedDateRange)
            {
                return AppConstants.TemporaryDisabilityRate;
            }

            return compensation.Event!.DisabilityRate!.Value;
        }
    }
}
