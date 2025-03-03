using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class MinimumWageRepository : GenericRepository<MinimumWage, AppDbContext>, IMinimumWageRepository
    {
        public MinimumWageRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<DateOnly> GetAvailableDateAsync()
            => await _context.MinimumWages.OrderBy(x => x.StartDate).Select(x => x.StartDate).FirstOrDefaultAsync();

        public async Task<List<MinimumWage>> GetMinimumWagesAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.MinimumWages.Where(x => x.IsActive)
                .Where(x => x.StartDate <= startDate && x.EndDate > endDate)
                .ToListAsync();
        }

        public async Task<MinimumWage> GetCurrentAsync()
            => await _context.MinimumWages.OrderByDescending(x => x.EndDate).FirstAsync();
    }
}
