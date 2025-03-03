using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Shared.Utilities;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.PeriodDatesSpecifier
{
    public class PassivePeriodDatesSpecifier : IPeriodDatesSpecifier
    {
        private readonly ILifeService _lifeService;

        public PassivePeriodDatesSpecifier(ILifeService lifeService)
        {
            _lifeService = lifeService;
        }

        public async Task<DateRangeDto> SpecifyAsync(CompensationDto compensation)
        {
            var passivePeriodStartDate = compensation.Claimant!.BirthDate.AddYears(AppConstants.RetirementAge);

            var currentDate = DateOnly.FromDateTime(compensation.CreatedDate);
            var currentAge = DateHelper.CalculateAge(compensation.Claimant.BirthDate, currentDate);
            var lifeTable = await _lifeService.FirstOrDefaultAsync(x =>
                                    x.IsActive
                                    &&
                                    x.LifeType == compensation.Event!.LifeTable
                                    &&
                                    x.Gender == compensation.Claimant.Gender
                                    &&
                                    x.CurrentAge == currentAge
                                  );

            var estimatedDays = (int)Math.Floor(lifeTable!.EstimatedYear * 360);
            var passivePeriodEndDate = currentDate.AddDays(estimatedDays);

            return new DateRangeDto
            {
                StartDate = passivePeriodStartDate,
                EndDate = passivePeriodEndDate
            };
        }
    }
}
