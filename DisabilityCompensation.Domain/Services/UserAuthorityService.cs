using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class UserAuthorityService : GenericService<IUserAuthorityRepository, UserAuthority>, IUserAuthorityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAuthorityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.UserAuthorityRepository, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> HasAuthorityAsync(Guid userId, ValueObjects.Authority authority)
        {
            var hasUserAuthority = await _unitOfWork.UserAuthorityRepository.HasAuthorityAsync(userId, authority);
            var hasRoleAuthority = await _unitOfWork.RoleAuthorityRepository.HasAuthorityAsync(userId, authority);
            var isAdmin = await _unitOfWork.UserRoleRepository.HasRoleAsync(userId, ValueObjects.Role.Admin);
            return isAdmin || hasRoleAuthority || hasUserAuthority;
        }

        public async Task<List<string>?> GetAuthoritiesAsync(Guid userId)
        {
            var isAdmin = await _unitOfWork.UserRoleRepository.HasRoleAsync(userId, ValueObjects.Role.Admin);
            if (isAdmin)
            {
                var authorities = await _unitOfWork.AuthorityRepository.FindAsync(x => x.IsActive);
                return authorities.Select(x => x.Name!).ToList();
            }

            var userAuthorities = await _unitOfWork.UserAuthorityRepository.GetAuthoritiesAsync(userId);
            var roleAuthorities = await _unitOfWork.RoleAuthorityRepository.GetAuthoritiesAsync(userId);

            if (roleAuthorities?.Any() == true)
            {
                return userAuthorities?.Concat(roleAuthorities).Distinct().ToList();
            }
            return userAuthorities;
        }
    }
}
