using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DateRangeCalculator
{
    public class ActivePeriodDateRangeCalculatorDto : IDateRangeCalculatorFactoryDto
    {
        public Periods Period { get; set; }
    }
}
