using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class CompensationRepository : GenericRepository<Compensation, AppDbContext>, ICompensationRepository
    {
        public CompensationRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
