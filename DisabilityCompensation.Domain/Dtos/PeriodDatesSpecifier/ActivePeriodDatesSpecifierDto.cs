using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier
{
    public class ActivePeriodDatesSpecifierDto : IPeriodDatesSpecifierFactoryDto
    {
        public Periods Period { get; set; }
    }
}
