using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface ICompensationService : IGenericService<Compensation>
    {
        Task<Guid> AddAsync(CompensationDto compensationDto);
    }
}
