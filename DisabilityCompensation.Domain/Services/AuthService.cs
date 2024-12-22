using AutoMapper;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class AuthService : GenericService<IAuthRepository, User>, IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.AuthRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto?> Login(string email, string password)
        {
            var user = await _unitOfWork.AuthRepository.Login(email, password);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
