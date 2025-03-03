namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator
{
    public interface ICompensationCalculator
    {
        Task<decimal> CalculateAsync(Guid compensationId);
    }
}
