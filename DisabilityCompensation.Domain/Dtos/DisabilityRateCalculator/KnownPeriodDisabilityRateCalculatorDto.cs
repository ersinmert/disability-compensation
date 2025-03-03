using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator
{
    public class KnownPeriodDisabilityRateCalculatorDto : IDisabilityRateCalculatorFactoryDto
    {
        public Periods Period { get; set; }
    }
}
