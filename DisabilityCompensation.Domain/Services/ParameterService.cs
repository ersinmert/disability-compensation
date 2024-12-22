using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IServices;

namespace DisabilityCompensation.Domain.Services
{
    public class ParameterService : GenericService<IParameterRepository, Parameter>, IParameterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParameterService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.ParameterRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Guid>> AddAsync(List<ParameterDto> parameterDtos)
        {
            var parameters = _mapper.Map<List<Parameter>>(parameterDtos);
            await _unitOfWork.ParameterRepository.AddRangeAsync(parameters);

            await _unitOfWork.SaveChangesAsync();

            return parameters.Select(x => x.Id).ToList();
        }

        public async Task<List<ParameterDto>> GetParameters(List<string>? codes)
        {
            var parameters = await _unitOfWork.ParameterRepository.GetParametersAsync(codes);
            var parametersDto = _mapper.Map<List<ParameterDto>>(parameters);

            return parametersDto;
        }
    }
}
