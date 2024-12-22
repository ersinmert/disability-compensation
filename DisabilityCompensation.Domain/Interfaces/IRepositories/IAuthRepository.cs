using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        Task<User?> Login(string email, string password);
    }
}
