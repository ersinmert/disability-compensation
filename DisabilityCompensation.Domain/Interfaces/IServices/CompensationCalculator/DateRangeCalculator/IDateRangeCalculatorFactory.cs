using DisabilityCompensation.Domain.Dtos.DateRangeCalculator;

namespace DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.DateRangeCalculator
{
    public interface IDateRangeCalculatorFactory
    {
        IDateRangeCalculator CreateCalculator(IDateRangeCalculatorFactoryDto calculatorFactoryDto);
    }
}
