using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Extensions;
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
                            .Include(x => x.CreatedByUser)
                            .Where(x => x.Id == id);

            return noTracking
                ? await query.AsNoTracking().FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync();
        }

        public async Task<PagedResult<Compensation>> SearchAllPagedAsync(SearchCompensationDto search)
        {
            var query = GetSearchQuery(search);

            var totalCount = await query.CountAsync();
            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((search.Page - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            return new PagedResult<Compensation>
            {
                Items = data,
                Page = search.Page,
                PageSize = search.PageSize,
                TotalRecords = totalCount,
                TotalPage = (int)Math.Ceiling((double)totalCount / search.PageSize)
            };
        }

        public async Task<PagedResult<Compensation>> SearchOwnedPagedAsync(SearchCompensationDto search, Guid userId)
        {
            var query = GetSearchQuery(search);
            query = query.Where(x => x.CreatedBy == userId);

            var totalCount = await query.CountAsync();
            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((search.Page - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            return new PagedResult<Compensation>
            {
                Items = data,
                Page = search.Page,
                PageSize = search.PageSize,
                TotalRecords = totalCount,
                TotalPage = (int)Math.Ceiling((double)totalCount / search.PageSize)
            };
        }

        private IQueryable<Compensation> GetSearchQuery(SearchCompensationDto search)
        {
            var startDate = search.Date?.ToUniversalTime().Date;
            var endDate = startDate?.AddDays(1);
            var query = _context.Compensations
                .Include(x => x.Claimant)
                .Include(x => x.Event)
                .Include(x => x.Documents)
                .Include(x => x.Expenses)
                .Include(x => x.CreatedByUser)
                .Where(x => x.IsActive)
                .WhereIf(search.Status != null, x => x.Status == search.Status)
                .WhereIf(search.Date != null && startDate != null && endDate != null, x =>
                    startDate <= x.CreatedDate
                    &&
                    x.CreatedDate < endDate);

            return query;
        }
    }
}
