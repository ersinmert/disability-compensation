using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier
{
    public interface IPeriodDatesSpecifier
    {
        Task<DateRangeDto> SpecifyAsync(CompensationDto compensation);
    }
}
