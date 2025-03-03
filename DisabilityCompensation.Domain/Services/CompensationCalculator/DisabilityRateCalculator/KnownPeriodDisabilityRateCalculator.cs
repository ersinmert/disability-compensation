using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DisabilityRateCalculator
{
    public class KnownPeriodDisabilityRateCalculator : IDisabilityRateCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public KnownPeriodDisabilityRateCalculator(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<decimal> CalculateAsync(CompensationDto compensation, DateRangeDto dateRange)
        {
            var knownPeriodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new KnownPeriodDatesSpecifierDto
            {
                Period = Periods.Known
            }).SpecifyAsync(compensation);
            var knownPeriodStartDate = knownPeriodDates.StartDate;
            var knownPeriodEndDate = knownPeriodDates.EndDate;

            if (compensation.HasTemporaryDisability != true)
            {
                return compensation.Event!.DisabilityRate!.Value;
            }

            var temporaryDisabilityEndDate = knownPeriodStartDate.AddDays(compensation!.TemporaryDisabilityDay!.Value);
            if (temporaryDisabilityEndDate > knownPeriodEndDate)
            {
                temporaryDisabilityEndDate = knownPeriodEndDate;
            }
            var hasMatchedDateRange = dateRange.StartDate <= temporaryDisabilityEndDate && dateRange.StartDate >= knownPeriodStartDate;
            if (hasMatchedDateRange)
            {
                return AppConstants.TemporaryDisabilityRate;
            }

            return compensation.Event!.DisabilityRate!.Value;
        }
    }
}
