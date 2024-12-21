using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class ParameterRepository : GenericRepository<Parameter, AppDbContext>, IParameterRepository
    {
        public ParameterRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
