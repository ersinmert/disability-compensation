using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class CompensationService : GenericService<ICompensationRepository, Compensation>, ICompensationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompensationService(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork.CompensationRepository, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddAsync(Compensation compensation)
        {
            await _unitOfWork.CompensationRepository.AddAsync(compensation);
            await _unitOfWork.SaveChangesAsync();

            return compensation.Id;
        }
    }
}
