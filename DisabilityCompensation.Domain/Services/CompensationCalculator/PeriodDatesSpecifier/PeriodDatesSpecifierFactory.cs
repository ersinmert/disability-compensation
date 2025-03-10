using DisabilityCompensation.Domain.Dtos.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.PeriodDatesSpecifier
{
    public class PeriodDatesSpecifierFactory : IPeriodDatesSpecifierFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public PeriodDatesSpecifierFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IPeriodDatesSpecifier CreateSpecifier(IPeriodDatesSpecifierFactoryDto calculatorFactoryDto)
        {
            switch (calculatorFactoryDto.Period)
            {
                case Periods.Known:
                    return new KnownPeriodDatesSpecifier();
                case Periods.Active:
                    return new ActivePeriodDatesSpecifier();
                case Periods.Passive:
                    return new PassivePeriodDatesSpecifier(_unitOfWork);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
