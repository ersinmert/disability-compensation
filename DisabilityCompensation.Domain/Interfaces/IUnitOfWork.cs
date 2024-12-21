using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;

namespace DisabilityCompensation.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitAsync();
        Task RollbackAsync();

        ICompensationRepository CompensationRepository { get; }
        IParameterRepository ParameterRepository { get; }
    }
}
