using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class CompensationCalculationManager : ICompensationCalculationManager
    {
        private readonly IList<ICompensationCalculator> _compensationCalculators;

        public CompensationCalculationManager(IList<ICompensationCalculator> compensationCalculators)
        {
            _compensationCalculators = compensationCalculators;
        }

        public async Task<decimal> CalculateAsync(Guid compensationId)
        {
            decimal totalAmount = 0;
            foreach (var compensationCalculator in _compensationCalculators)
            {
                decimal amount = await compensationCalculator.CalculateAsync(compensationId);
                totalAmount += amount;
            }
            return totalAmount;
        }
    }
}
