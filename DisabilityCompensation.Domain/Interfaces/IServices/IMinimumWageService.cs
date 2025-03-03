using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IMinimumWageService : IGenericService<MinimumWage>
    {
        Task<List<MinimumWageDto>> GetMinimumWagesAsync(DateOnly startDate, DateOnly endDate);
        Task<MinimumWageDto> GetCurrentAsync();
    }
}
