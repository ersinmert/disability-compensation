using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using DisabilityCompensation.Shared.Extensions;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class UserAuthorityRepository : GenericRepository<UserAuthority, AppDbContext>, IUserAuthorityRepository
    {
        public UserAuthorityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> HasAuthorityAsync(Guid userId, Domain.ValueObjects.Authority authority)
        {
            var authorityName = authority.GetDescription();

            return await _context.UserAuthorities
                .Include(ua => ua.Authority)
                .AnyAsync(ua => ua.UserId == userId && ua.Authority!.Name == authorityName && ua.IsActive);
        }

        public async Task<List<string>?> GetAuthoritiesAsync(Guid userId)
        {
            return await _context.UserAuthorities
                .Include(ua => ua.Authority)
                .Where(x => x.IsActive && x.UserId == userId)
                .Select(x => x.Authority!.Name!)
                .ToListAsync();
        }
    }
}
