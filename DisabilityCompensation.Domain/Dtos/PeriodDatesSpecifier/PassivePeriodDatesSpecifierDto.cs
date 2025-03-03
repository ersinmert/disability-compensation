using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier
{
    public class PassivePeriodDatesSpecifierDto : IPeriodDatesSpecifierFactoryDto
    {
        public Periods Period { get; set; }
    }
}
