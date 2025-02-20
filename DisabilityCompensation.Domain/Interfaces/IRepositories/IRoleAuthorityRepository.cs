using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IRoleAuthorityRepository : IGenericRepository<RoleAuthority>
    {
        Task<bool> HasAuthorityAsync(Guid userId, ValueObjects.Authority authority);
        Task<List<string>?> GetAuthoritiesAsync(Guid userId);
    }
}
