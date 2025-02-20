using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class UserRoleService : GenericService<IUserRoleRepository, UserRole>, IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.UserRoleRepository, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsAdmin(Guid userId)
        {
            return await _unitOfWork.UserRoleRepository.HasRoleAsync(userId, ValueObjects.Role.Admin);
        }
    }
}
