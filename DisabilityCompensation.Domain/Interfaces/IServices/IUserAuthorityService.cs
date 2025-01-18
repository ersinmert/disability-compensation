using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IUserAuthorityService : IGenericService<UserAuthority>
    {
        Task<bool> HasAuthorityAsync(Guid userId, ValueObjects.Authority authority);
        Task<List<string>?> GetAuthoritiesAsync(Guid userId);
    }
}
