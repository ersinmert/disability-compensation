using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class AuthRepository : GenericRepository<User, AppDbContext>, IAuthRepository
    {
        public AuthRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<User?> Login(string email, string password)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.IsActive && x.Email == email && x.Password == password);
        }
    }
}
