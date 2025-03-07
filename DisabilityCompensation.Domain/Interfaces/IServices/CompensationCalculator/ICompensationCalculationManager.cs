using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator
{
    public interface ICompensationCalculationManager
    {
        Task<CompensationCalculatorResultDto> CalculateAsync(Guid compensationId);
    }
}
