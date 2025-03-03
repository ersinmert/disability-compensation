using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DateRangeCalculator
{
    public class ActivePeriodDateRangeCalculator : BaseDateRangeCalculator, IDateRangeCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public ActivePeriodDateRangeCalculator(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<HashSet<DateRangeDto>> GetDateRangesAsync(CompensationDto compensation)
        {
            var periodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new ActivePeriodDatesSpecifierDto
            {
                Period = Shared.Dtos.Enums.Periods.Active
            }).SpecifyAsync(compensation);
            var activePeriodStartDate = periodDates.StartDate;
            var activePeriodEndDate = periodDates.EndDate;
            var dateRanges = GetDateRanges(activePeriodStartDate, activePeriodEndDate);

            if (compensation.HasTemporaryDisability == true)
            {
                var temporaryDisabilityStartDate = DateOnly.FromDateTime(compensation!.Event!.EventDate);
                var temporaryDisabilityEndDate = temporaryDisabilityStartDate.AddDays(compensation!.TemporaryDisabilityDay!.Value);
                if (temporaryDisabilityEndDate > activePeriodStartDate)
                {
                    AddTemporaryDisabilityDatesToDateRanges(dateRanges, temporaryDisabilityEndDate);
                }
            }
            return dateRanges;
        }
    }
}
