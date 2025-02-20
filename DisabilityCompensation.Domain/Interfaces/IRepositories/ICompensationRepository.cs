using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Domain.Interfaces.IRepositories
{
    public interface ICompensationRepository : IGenericRepository<Compensation>
    {
        Task<PagedResult<Compensation>> SearchAllPagedAsync(SearchCompensationDto search);
        Task<PagedResult<Compensation>> SearchOwnedPagedAsync(SearchCompensationDto search, Guid userId);
    }
}
