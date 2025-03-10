using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public class CompensationCalculationManager : ICompensationCalculationManager
    {
        private readonly IList<ICompensationCalculator> _compensationCalculators;

        public CompensationCalculationManager(IEnumerable<ICompensationCalculator> compensationCalculators)
        {
            _compensationCalculators = compensationCalculators.ToList();
        }

        public async Task<CompensationCalculatorResultDto> CalculateAsync(Guid compensationId)
        {
            decimal totalAmount = 0;
            List<CompensationCalculationDto> compensationCalculations = new List<CompensationCalculationDto>();
            foreach (var compensationCalculator in _compensationCalculators)
            {
                CompensationCalculatorResultDto calculateResult = await compensationCalculator.CalculateAsync(compensationId);
                totalAmount += calculateResult.Amount;
                compensationCalculations.AddRange(calculateResult.CompensationCalculations);
            }
            return new CompensationCalculatorResultDto
            {
                CompensationCalculations = compensationCalculations,
                Amount = totalAmount
            };
        }
    }
}
