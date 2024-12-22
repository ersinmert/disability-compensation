using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IRepositories;

namespace DisabilityCompensation.Application.Interfaces
{
    public interface IParameterRepository : IGenericRepository<Parameter>
    {
        Task<List<Parameter>> GetParametersAsync(List<string>? codes);
    }
}
