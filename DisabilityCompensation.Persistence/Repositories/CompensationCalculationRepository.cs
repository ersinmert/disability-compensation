using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class CompensationCalculationRepository : GenericRepository<CompensationCalculation, AppDbContext>, ICompensationCalculationRepository
    {
        public CompensationCalculationRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
