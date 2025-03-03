using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IMinimumWageRepository : IGenericRepository<MinimumWage>
    {
        Task<DateOnly> GetAvailableDateAsync();
        Task<List<MinimumWage>> GetMinimumWagesAsync(DateOnly startDate, DateOnly endDate);
        Task<MinimumWage> GetCurrentAsync();
    }
}
