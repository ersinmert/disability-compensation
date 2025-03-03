using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Constants;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.PeriodDatesSpecifier
{
    public class ActivePeriodDatesSpecifier : IPeriodDatesSpecifier
    {
        public async Task<DateRangeDto> SpecifyAsync(CompensationDto compensation)
        {
            var activePeriodStartDate = DateOnly.FromDateTime(compensation.CreatedDate);
            var activePeriodEndDate = compensation.Claimant!.BirthDate.AddYears(AppConstants.RetirementAge);

            return await Task.FromResult(new DateRangeDto
            {
                StartDate = activePeriodStartDate,
                EndDate = activePeriodEndDate
            });
        }
    }
}
