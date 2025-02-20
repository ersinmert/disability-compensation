using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class RoleAuthorityRepository : GenericRepository<RoleAuthority, AppDbContext>, IRoleAuthorityRepository
    {
        public RoleAuthorityRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<string>?> GetAuthoritiesAsync(Guid userId)
        {
            var userRoles = await _context.UserRoles
                .Where(x => x.UserId == userId && x.IsActive)
                .Select(x => x.RoleId)
                .ToListAsync();

            return await _context.RoleAuthorities
                .Include(x => x.Authority)
                .Where(x => userRoles.Contains(x.RoleId) && x.IsActive)
                .Select(x => x.Authority!.Name!)
                .ToListAsync();
        }

        public async Task<bool> HasAuthorityAsync(Guid userId, Domain.ValueObjects.Authority authority)
        {
            var authorityName = authority.GetDescription();

            return await _context.UserRoles
                .Include(x => x.Role).ThenInclude(x => x!.RoleAuthorities!).ThenInclude(x => x.Authority)
                .Where(ur => ur.UserId == userId && ur.IsActive)
                .AnyAsync(ur => ur.Role!.RoleAuthorities!.Any(ra => ra.Authority!.Name == authorityName && ra.IsActive));
        }
    }
}
