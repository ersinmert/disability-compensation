using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Shared.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IUserService : IGenericService<User>
    {
        Task<PagedResultDto<UserDto>> SearchPagedAsync(SearchUserDto request);
        Task<Guid> AddAsync(UserDto userDto, UserClaim userClaim);
        Task<bool> DeleteAsync(Guid id, UserClaim userClaim);
        Task<UserDto> UpdateAsync(UpdateUserDto userDto, UserClaim userClaim);
    }
}
