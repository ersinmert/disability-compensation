using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator
{
    public class ActivePeriodDisabilityRateCalculatorDto : IDisabilityRateCalculatorFactoryDto
    {
        public Periods Period { get; set; }
    }
}
