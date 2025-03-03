using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier
{
    public interface IPeriodDatesSpecifierFactory
    {
        IPeriodDatesSpecifier CreateSpecifier(IPeriodDatesSpecifierFactoryDto calculatorFactoryDto);
    }
}
