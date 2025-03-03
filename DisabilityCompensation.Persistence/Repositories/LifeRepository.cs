using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class LifeRepository : GenericRepository<Life, AppDbContext>, ILifeRepository
    {
        public LifeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
