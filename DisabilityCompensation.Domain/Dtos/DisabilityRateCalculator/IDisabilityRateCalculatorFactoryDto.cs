using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator
{
    public interface IDisabilityRateCalculatorFactoryDto
    {
        Periods Period { get; set; }
    }
}
