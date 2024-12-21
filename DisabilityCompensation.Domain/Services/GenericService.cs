using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;
using System.Linq.Expressions;

namespace DisabilityCompensation.Domain.Services
{
    public class GenericService<TRepository, TEntity> : IGenericService<TEntity> where TEntity : class, IEntity
                                                                                 where TRepository : IGenericRepository<TEntity>
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async virtual Task<TEntity?> GetByIdAsync(Guid id, bool noTracking = false) => await _repository.GetByIdAsync(id, noTracking);

        public async virtual Task<TDto?> GetByIdAsync<TDto>(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id, false);
            return _mapper.Map<TDto>(entity);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public async virtual Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
            => await _repository.FirstOrDefaultAsync(predicate, noTracking);

        public async virtual Task<TDto?> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            var entity = await _repository.FirstOrDefaultAsync(predicate, noTracking);
            return _mapper.Map<TDto>(entity);
        }

        public async virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
            => await _repository.FindAsync(predicate, noTracking);

        public async virtual Task<IEnumerable<TDto>> FindAsync<TDto>(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            var entities = await _repository.FindAsync(predicate, noTracking);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => await _repository.AnyAsync(predicate);
    }
}
