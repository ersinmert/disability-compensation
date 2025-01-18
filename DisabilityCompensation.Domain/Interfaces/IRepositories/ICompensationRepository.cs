using DisabilityCompensation.Domain.Entities;
using System.Linq.Expressions;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface ICompensationRepository : IGenericRepository<Compensation>
    {
        Task<IList<Compensation>> GetCompensationsAsync(Expression<Func<Compensation, bool>> predicate, bool noTracking = false);
    }
}
