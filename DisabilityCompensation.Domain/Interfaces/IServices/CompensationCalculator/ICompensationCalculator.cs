using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator
{
    public interface ICompensationCalculator
    {
        Task<CompensationCalculatorResultDto> CalculateAsync(Guid compensationId);
    }
}
