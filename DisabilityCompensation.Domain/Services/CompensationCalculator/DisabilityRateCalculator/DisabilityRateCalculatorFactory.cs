using DisabilityCompensation.Domain.Dtos.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DisabilityRateCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DisabilityRateCalculator
{
    public class DisabilityRateCalculatorFactory : IDisabilityRateCalculatorFactory
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public DisabilityRateCalculatorFactory(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public IDisabilityRateCalculator CreateCalculator(IDisabilityRateCalculatorFactoryDto disabilityRateCalculatorDto)
        {
            switch (disabilityRateCalculatorDto.Period)
            {
                case Periods.Known:
                    return new KnownPeriodDisabilityRateCalculator(_periodDatesSpecifierFactory);
                case Periods.Active:
                    return new ActivePeriodDisabilityRateCalculator(_periodDatesSpecifierFactory);
                case Periods.Passive:
                    return new PassivePeriodDisabilityRateCalculator(_periodDatesSpecifierFactory);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
