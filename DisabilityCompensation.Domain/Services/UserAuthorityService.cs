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
            return await _unitOfWork.UserAuthorityRepository.HasAuthorityAsync(userId, authority);
        }

        public async Task<List<string>?> GetAuthoritiesAsync(Guid userId)
        {
            return await _unitOfWork.UserAuthorityRepository.GetAuthoritiesAsync(userId);
        }
    }
}
