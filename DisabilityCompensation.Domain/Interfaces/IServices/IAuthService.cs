using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IAuthService : IGenericService<User>
    {
        Task<UserDto?> Login(string email, string password);
    }
}
