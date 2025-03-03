using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.DateRangeCalculator
{
    public interface IDateRangeCalculatorFactoryDto
    {
        Periods Period { get; set; }
    }
}
