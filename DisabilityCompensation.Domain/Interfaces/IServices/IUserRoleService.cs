using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IUserRoleService : IGenericService<UserRole>
    {
        Task<bool> IsAdmin(Guid userId);
    }
}
