using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.PeriodDatesSpecifier
{
    public class PeriodDatesSpecifierFactory : IPeriodDatesSpecifierFactory
    {
        private readonly ILifeService _lifeService;

        public PeriodDatesSpecifierFactory(ILifeService lifeService)
        {
            _lifeService = lifeService;
        }

        public IPeriodDatesSpecifier CreateSpecifier(IPeriodDatesSpecifierFactoryDto calculatorFactoryDto)
        {
            switch (calculatorFactoryDto.Period)
            {
                case Periods.Known:
                    return new KnownPeriodDatesSpecifier();
                case Periods.Active:
                    return new ActivePeriodDatesSpecifier();
                case Periods.Passive:
                    return new PassivePeriodDatesSpecifier(_lifeService);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
