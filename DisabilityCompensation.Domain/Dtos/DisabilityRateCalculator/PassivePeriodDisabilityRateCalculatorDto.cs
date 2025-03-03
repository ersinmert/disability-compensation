using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator
{
    public class PassivePeriodDisabilityRateCalculatorDto : IDisabilityRateCalculatorFactoryDto
    {
        public Periods Period { get; set; }
    }
}
