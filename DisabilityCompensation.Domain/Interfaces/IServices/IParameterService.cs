using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IParameterService : IGenericService<Parameter>
    {
        Task<List<Guid>> AddAsync(List<ParameterDto> parameterDtos);
        Task<List<ParameterDto>> GetParameters(List<string>? codes);
    }
}
