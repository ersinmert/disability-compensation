using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IUserAuthorityRepository : IGenericRepository<UserAuthority>
    {
        Task<bool> HasAuthorityAsync(Guid userId, ValueObjects.Authority authority);
        Task<List<string>?> GetAuthoritiesAsync(Guid userId);
    }
}
