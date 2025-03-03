using AutoMapper;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Exceptions;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class MinimumWageService : GenericService<IMinimumWageRepository, MinimumWage>, IMinimumWageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MinimumWageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.MinimumWageRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MinimumWageDto>> GetMinimumWagesAsync(DateOnly startDate, DateOnly endDate)
        {
            var dateRangesWages = new List<(DateOnly, DateOnly, decimal)>();
            var availableDate = await _unitOfWork.MinimumWageRepository.GetAvailableDateAsync();
            if (startDate < availableDate)
            {
                throw new NotValidException($"Minimum wage data is not available before {availableDate}");
            }

            var data = await _unitOfWork.MinimumWageRepository.GetMinimumWagesAsync(startDate, endDate);
            return _mapper.Map<List<MinimumWageDto>>(data);
        }

        public async Task<MinimumWageDto> GetCurrentAsync()
        {
            var data = await _unitOfWork.MinimumWageRepository.GetCurrentAsync();
            return _mapper.Map<MinimumWageDto>(data);
        }
    }
}
