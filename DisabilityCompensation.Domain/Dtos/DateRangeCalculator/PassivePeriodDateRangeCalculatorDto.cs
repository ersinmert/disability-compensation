using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DateRangeCalculator
{
    public class PassivePeriodDateRangeCalculatorDto : IDateRangeCalculatorFactoryDto
    {
        public Periods Period { get; set; }
    }
}
