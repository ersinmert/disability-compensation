using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class CompensationService : GenericService<ICompensationRepository, Compensation>, ICompensationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompensationService(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork.CompensationRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(CompensationDto compensationDto)
        {
            if (compensationDto.Documents?.Any() == true)
            {
                compensationDto.Documents.ForEach(x => x.FilePath = "filePath");
            }
            var compensation = _mapper.Map<Compensation>(compensationDto);
            await _unitOfWork.CompensationRepository.AddAsync(compensation);
            await _unitOfWork.SaveChangesAsync();

            return compensationDto.Id;
        }
    }
}
