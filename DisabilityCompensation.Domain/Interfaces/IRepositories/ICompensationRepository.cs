using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface ICompensationRepository : IGenericRepository<Compensation>
    {
        Task<PagedResult<Compensation>> SearchCompensationsAsync(SearchCompensationDto search);
    }
}
