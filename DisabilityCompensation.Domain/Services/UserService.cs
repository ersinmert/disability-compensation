using AutoMapper;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Exceptions;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Extensions;

namespace DisabilityCompensation.Domain.Services
{
    public class UserService : GenericService<IUserRepository, User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.UserRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<UserDto>> SearchPagedAsync(SearchUserDto request)
        {
            var users = await _unitOfWork.UserRepository.SearchPagedAsync(request);
            var usersDto = _mapper.Map<PagedResultDto<UserDto>>(users);

            return usersDto;
        }

        public async Task<Guid> AddAsync(UserDto userDto, UserClaim userClaim)
        {
            var user = _mapper.Map<User>(userDto);
            user.CreatedBy = userClaim.UserId;

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, UserClaim userClaim)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, true);
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı!");
            }
            _unitOfWork.UserRepository.SoftRemove(user);
            user.UpdatedBy = userClaim.UserId;
            user.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto userDto, UserClaim userClaim)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userDto.Id, true);
            if (user == null)
                throw new NotFoundException("Kullanıcı bulunamadı!");

            ClassExtension.MapProperties(userDto, user);
            user.UpdatedDate = DateTime.UtcNow;
            user.UpdatedBy = userClaim.UserId;

            _unitOfWork.UserRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}
