using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class AuthorityRepository : GenericRepository<Authority, AppDbContext>, IAuthorityRepository
    {
        public AuthorityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
