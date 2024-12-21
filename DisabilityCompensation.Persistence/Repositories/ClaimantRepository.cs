using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class ClaimantRepository : GenericRepository<Claimant, AppDbContext>, IClaimantRepository
    {
        public ClaimantRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
