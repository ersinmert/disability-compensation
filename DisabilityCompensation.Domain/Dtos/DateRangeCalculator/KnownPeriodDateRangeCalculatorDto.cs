using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DateRangeCalculator
{
    public class KnownPeriodDateRangeCalculatorDto : IDateRangeCalculatorFactoryDto
    {
        public Periods Period { get; set; }
    }
}
