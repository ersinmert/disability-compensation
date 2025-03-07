namespace DisabilityCompensation.Domain.Dtos
{
    public class CompensationCalculatorResultDto
    {
        public List<CompensationCalculationDto> CompensationCalculations { get; set; } = new List<CompensationCalculationDto>();

        public decimal Amount { get; set; }
    }
}
