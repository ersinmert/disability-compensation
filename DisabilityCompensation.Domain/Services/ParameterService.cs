using AutoMapper;
using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class ParameterService : GenericService<IParameterRepository, Parameter>, IParameterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParameterService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.ParameterRepository, mapper)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
