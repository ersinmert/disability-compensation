using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator
{
    public interface IDisabilityRateCalculator
    {
        Task<decimal> CalculateAsync(CompensationDto compensation, DateRangeDto dateRange);
    }
}
