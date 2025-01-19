using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DisabilityCompensation.Persistence.Repositories
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class, IEntity where TContext : DbContext
    {
        protected readonly TContext _context;

        protected GenericRepository(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public Task<int> ExecuteSqlAsync(string query)
        {
            return _context.Database.ExecuteSqlRawAsync(query);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            if (noTracking)
                return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            if (noTracking)
                return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().Where(predicate);

            return noTracking
                ? await query.AsNoTracking().ToListAsync()
                : await query.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool noTracking = false)
        {
            var query = _context.Set<TEntity>();

            return noTracking
                ? await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
                : await query.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            var query = _context.Set<TEntity>();

            return noTracking
                ? await query.AsNoTracking().SingleOrDefaultAsync(predicate)
                : await query.SingleOrDefaultAsync(predicate);
        }

        public void SoftRemove(TEntity entity)
        {
            var property = entity.GetType().GetProperty("IsActive");

            var propertyValue = (bool?)property?.GetValue(entity);

            if (property != null && propertyValue.HasValue)
            {
                _context.Entry(entity).State = EntityState.Modified;
                property.SetValue(entity, false);
            }
        }

        public void SoftRemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                var property = entity.GetType().GetProperty("IsActive");

                var propertyValue = (int?)property?.GetValue(entity);

                if (property != null && propertyValue.HasValue)
                {
                    property.SetValue(entity, false);
                }
            }
        }

        public void Update(TEntity entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
