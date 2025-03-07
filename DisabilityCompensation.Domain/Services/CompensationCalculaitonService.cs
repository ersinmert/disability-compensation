using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class CompensationCalculaitonService : GenericService<ICompensationCalculationRepository, CompensationCalculation>, ICompensationCalculaitonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompensationCalculaitonService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.CompensationCalculationRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
