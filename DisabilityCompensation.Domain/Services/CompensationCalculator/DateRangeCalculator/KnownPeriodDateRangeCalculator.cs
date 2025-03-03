using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DateRangeCalculator
{
    public class KnownPeriodDateRangeCalculator : BaseDateRangeCalculator, IDateRangeCalculator
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public KnownPeriodDateRangeCalculator(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public async Task<HashSet<DateRangeDto>> GetDateRangesAsync(CompensationDto compensation)
        {
            var periodDates = await _periodDatesSpecifierFactory.CreateSpecifier(new KnownPeriodDatesSpecifierDto
            {
                Period = Shared.Dtos.Enums.Periods.Known
            }).SpecifyAsync(compensation);
            var knownPeriodStartDate = periodDates.StartDate;
            var knownPeriodEndDate = periodDates.EndDate;

            var birthDate = compensation.Claimant!.BirthDate;

            var dateRanges = GetDateRanges(knownPeriodStartDate, knownPeriodEndDate);
            AddBirthDatesToDateRanges(dateRanges, knownPeriodStartDate, knownPeriodEndDate, birthDate);
            if (compensation.HasTemporaryDisability == true)
            {
                var temporaryDisabilityEndDate = knownPeriodStartDate.AddDays(compensation!.TemporaryDisabilityDay!.Value);
                var hasOverflowMaximumEndDate = temporaryDisabilityEndDate > knownPeriodEndDate;
                if (hasOverflowMaximumEndDate)
                {
                    temporaryDisabilityEndDate = knownPeriodEndDate;
                }
                AddTemporaryDisabilityDatesToDateRanges(dateRanges, temporaryDisabilityEndDate);
            }

            return dateRanges;
        }

        private void AddBirthDatesToDateRanges(HashSet<DateRangeDto> dateRanges, DateOnly knownPeriodStartDate, DateOnly knownPeriodEndDate, DateOnly birthDate)
        {
            var birthDay = new DateOnly(knownPeriodStartDate.Year, birthDate.Month, birthDate.Day);
            while (birthDay <= knownPeriodEndDate)
            {
                var dataRange = dateRanges.FirstOrDefault(x => x.StartDate < birthDay && birthDay < x.EndDate);
                if (dataRange != null)
                {
                    dateRanges.Remove(dataRange);
                    dateRanges.Add(new DateRangeDto
                    {
                        StartDate = dataRange.StartDate,
                        EndDate = birthDay
                    });
                    dateRanges.Add(new DateRangeDto
                    {
                        StartDate = birthDay,
                        EndDate = dataRange.EndDate
                    });
                }
                birthDay = birthDay.AddYears(1);
            }
        }
    }
}
