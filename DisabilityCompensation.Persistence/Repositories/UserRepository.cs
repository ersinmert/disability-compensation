using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<PagedResult<User>> SearchPagedAsync(SearchUserDto search)
        {
            var query = _context.Users
                .WhereIf(!string.IsNullOrEmpty(search.Name), user => user.Name == search.Name)
                .WhereIf(!string.IsNullOrEmpty(search.Surname), user => user.Surname == search.Surname)
                .WhereIf(!string.IsNullOrEmpty(search.Email), user => user.Email == search.Email)
                .Where(user => user.IsActive);

            var totalCount = await query.CountAsync();
            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((search.Page - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            return new PagedResult<User>
            {
                Items = data,
                Page = search.Page,
                PageSize = search.PageSize,
                TotalRecords = totalCount,
                TotalPage = (int)Math.Ceiling((double)totalCount / search.PageSize)
            };
        }
    }
}
