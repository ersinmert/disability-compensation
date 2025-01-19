using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class AuthorityService : GenericService<IAuthorityRepository, Authority>, IAuthorityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorityService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.AuthorityRepository, mapper)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
