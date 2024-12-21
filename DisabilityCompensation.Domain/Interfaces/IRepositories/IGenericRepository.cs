using DisabilityCompensation.Domain.Entities;
using System.Linq.Expressions;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool noTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<int> ExecuteSqlAsync(string query);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void SoftRemove(TEntity entity);
        void SoftRemoveRange(IEnumerable<TEntity> entities);
        Task<bool> SaveChangesAsync();
    }
}
