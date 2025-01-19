using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PagedResult<User>> SearchPagedAsync(SearchUserDto search);
    }
}
