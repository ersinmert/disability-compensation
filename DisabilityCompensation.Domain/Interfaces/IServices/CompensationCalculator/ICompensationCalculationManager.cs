namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator
{
    public interface ICompensationCalculationManager
    {
        Task<decimal> CalculateAsync(Guid compensationId);
    }
}
