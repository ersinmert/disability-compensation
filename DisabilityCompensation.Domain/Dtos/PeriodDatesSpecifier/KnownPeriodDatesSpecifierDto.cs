using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier
{
    public class KnownPeriodDatesSpecifierDto : IPeriodDatesSpecifierFactoryDto
    {
        public Periods Period { get; set; }
    }
}
