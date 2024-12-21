using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Persistence.Contexts;

namespace DisabilityCompensation.Persistence.Repositories
{
    public class EventRepository : GenericRepository<Event, AppDbContext>, IEventRepository
    {
        public EventRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
