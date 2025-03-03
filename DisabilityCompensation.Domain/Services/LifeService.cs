using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class LifeService : GenericService<ILifeRepository, Life>, ILifeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LifeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.LifeRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
