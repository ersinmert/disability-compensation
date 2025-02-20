using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole, AppDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> HasRoleAsync(Guid userId, Domain.ValueObjects.Role role)
        {
            var roleName = role.GetDescription();
            return await _context.Users.Where(x => x.Id == userId && x.IsActive)
                .Include(x => x.UserRoles!).ThenInclude(x => x.Role)
                .AnyAsync(x => x.UserRoles!.Any(ur => ur.Role!.Name == roleName && ur.IsActive));
        }

        public async Task<List<Domain.ValueObjects.Role>> GetRolesAsync(Guid userId)
        {
            var roles = await _context.UserRoles.Where(x => x.UserId == userId && x.IsActive)
                .Include(x => x.Role)
                .Select(x => x.Role!.Name)
                .ToListAsync();

            return roles.Select(role => role!.GetEnumByDescription<Domain.ValueObjects.Role>()).ToList();
        }
    }
}
