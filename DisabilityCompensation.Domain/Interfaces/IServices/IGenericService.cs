using DisabilityCompensation.Domain.Entities;
using System.Linq.Expressions;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IGenericService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool noTracking = false);
        Task<TDto?> GetByIdAsync<TDto>(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TDto>> GetAllAsync<TDto>();
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<IEnumerable<TDto>> FindAsync<TDto>(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
