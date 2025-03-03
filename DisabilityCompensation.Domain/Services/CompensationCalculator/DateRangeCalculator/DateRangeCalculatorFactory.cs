using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DateRangeCalculator
{
    public class DateRangeCalculatorFactory : IDateRangeCalculatorFactory
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public DateRangeCalculatorFactory(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public IDateRangeCalculator CreateCalculator(IDateRangeCalculatorFactoryDto calculatorFactoryDto)
        {
            switch (calculatorFactoryDto.Period)
            {
                case Periods.Known:
                    return new KnownPeriodDateRangeCalculator(_periodDatesSpecifierFactory);
                case Periods.Active:
                    return new ActivePeriodDateRangeCalculator(_periodDatesSpecifierFactory);
                case Periods.Passive:
                    return new PassivePeriodDateRangeCalculator(_periodDatesSpecifierFactory);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
