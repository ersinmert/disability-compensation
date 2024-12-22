using DisabilityCompensation.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using DisabilityCompensation.Persistence.Contexts;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;

namespace DisabilityCompensation.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(
            AppDbContext context,
            ICompensationRepository compensationRepository,
            IParameterRepository parameterRepository,
            IAuthRepository authRepository)
        {
            _context = context;
            CompensationRepository = compensationRepository;
            ParameterRepository = parameterRepository;
            AuthRepository = authRepository;
        }

        #region Methods

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }

        #endregion

        public ICompensationRepository CompensationRepository { get; set; }
        public IParameterRepository ParameterRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
    }

}
