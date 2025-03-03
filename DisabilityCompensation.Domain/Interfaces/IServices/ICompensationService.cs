using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Shared.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface ICompensationService : IGenericService<Compensation>
    {
        Task<Guid> AddAsync(CompensationDto compensationDto, UserClaim userClaim);
        Task<PagedResultDto<CompensationDto>> SearchPagedAsync(SearchCompensationDto search, UserClaim userClaim);
        Task<bool> ApproveAsync(ApproveCompensationDto approveDto, UserClaim userClaim);
        Task<bool> RejectAsync(RejectCompensationDto rejectDto, UserClaim userClaim);
        Task CalculateAsync(Guid compensationId);
    }
}
