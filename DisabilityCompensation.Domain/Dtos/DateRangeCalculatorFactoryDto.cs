using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos
{
    public class DateRangeCalculatorFactoryDto
    {
        public List<LifeDto>? LifeTable { get; set; }
        public Periods Period { get; set; }
    }
}
