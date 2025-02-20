using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<bool> HasRoleAsync(Guid userId, ValueObjects.Role role);
        Task<List<Domain.ValueObjects.Role>> GetRolesAsync(Guid userId);
    }
}
