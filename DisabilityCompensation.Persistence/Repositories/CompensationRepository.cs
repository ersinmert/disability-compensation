using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class CompensationRepository : GenericRepository<Compensation, AppDbContext>, ICompensationRepository
    {
        public CompensationRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public override async Task<Compensation?> GetByIdAsync(Guid id, bool noTracking = false)
        {
            var query = _context.Compensations
                            .Include(x => x.Claimant)
                            .Include(x => x.Event)
                            .Include(x => x.Documents)
                            .Include(x => x.Expenses)
                            .Where(x => x.Id == id);

            return noTracking
                ? await query.AsNoTracking().FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync();
        }
    }
}
