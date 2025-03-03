using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator
{
    public interface IDateRangeCalculator
    {
        Task<HashSet<DateRangeDto>> GetDateRangesAsync(CompensationDto compensation);
    }
}
