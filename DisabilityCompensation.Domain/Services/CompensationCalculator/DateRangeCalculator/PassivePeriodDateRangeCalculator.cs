using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DateRangeCalculator
{
    public class PassivePeriodDateRangeCalculator : BaseDateRangeCalculator, IDateRangeCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public PassivePeriodDateRangeCalculator(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<HashSet<DateRangeDto>> GetDateRangesAsync(CompensationDto compensation)
        {
            var periodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new PassivePeriodDatesSpecifierDto
            {
                Period = Periods.Passive
            }).SpecifyAsync(compensation);

            var passivePeriodStartDate = periodDates.StartDate;
            var passivePeriodEndDate = periodDates.EndDate;
            var dateRanges = GetDateRanges(passivePeriodStartDate, passivePeriodEndDate);

            if (compensation.HasTemporaryDisability == true)
            {
                var temporaryDisabilityStartDate = DateOnly.FromDateTime(compensation!.Event!.EventDate);
                var temporaryDisabilityEndDate = temporaryDisabilityStartDate.AddDays(compensation!.TemporaryDisabilityDay!.Value);
                if (temporaryDisabilityEndDate > passivePeriodStartDate)
                {
                    AddTemporaryDisabilityDatesToDateRanges(dateRanges, temporaryDisabilityEndDate);
                }
            }
            return dateRanges;
        }
    }
}
