using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.PeriodDatesSpecifier
{
    public class KnownPeriodDatesSpecifier : IPeriodDatesSpecifier
    {
        public async Task<DateRangeDto> SpecifyAsync(CompensationDto compensation)
        {
            var knownPeriodStartDate = DateOnly.FromDateTime(compensation!.Event!.EventDate);
            var knownPeriodEndDate = DateOnly.FromDateTime(compensation.CreatedDate);

            return await Task.FromResult(new DateRangeDto
            {
                StartDate = knownPeriodStartDate,
                EndDate = knownPeriodEndDate
            });
        }
    }
}
