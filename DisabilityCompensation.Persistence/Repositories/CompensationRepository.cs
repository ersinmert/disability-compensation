using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<IList<Compensation>> GetCompensationsAsync(Expression<Func<Compensation, bool>> predicate, bool noTracking = false)
        {
            var query = _context.Compensations
                .Include(x => x.Claimant)
                .Include(x => x.Event)
                .Include(x => x.Documents)
                .Include(x => x.Expenses)
                .Where(predicate);

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }
    }
}
