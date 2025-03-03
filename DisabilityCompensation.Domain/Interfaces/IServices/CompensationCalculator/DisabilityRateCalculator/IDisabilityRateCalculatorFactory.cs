using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator
{
    public interface IDisabilityRateCalculatorFactory
    {
        IDisabilityRateCalculator CreateCalculator(IDisabilityRateCalculatorFactoryDto disabilityRateCalculatorDto);
    }
}
