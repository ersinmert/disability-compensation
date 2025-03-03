using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.PeriodDatesSpecifier;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Shared.Dtos.Enums;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class SalaryCalculatorFactory : ISalaryCalculatorFactory
    {
        private readonly IPeriodDatesSpecifierFactory _periodDatesSpecifierFactory;

        public SalaryCalculatorFactory(IPeriodDatesSpecifierFactory periodDatesSpecifierFactory)
        {
            _periodDatesSpecifierFactory = periodDatesSpecifierFactory;
        }

        public ISalaryCalculator CreateCalculator(ISalaryCalculatorDto salaryCalculatorDto)
        {
            switch (salaryCalculatorDto.Period)
            {
                case Periods.Known:
                    {
                        switch (salaryCalculatorDto.SalaryCalculatorType)
                        {
                            case SalaryCalculatorTypes.Claimant:
                                var claimantDto = (KnownPeriodClaimantSalaryCalculatorDto)salaryCalculatorDto;
                                return new KnownPeriodClaimantSalaryCalculator(claimantDto);
                            case SalaryCalculatorTypes.Caregiver:
                                var caregiverDto = (KnownPeriodCaregiverSalaryCalculatorDto)salaryCalculatorDto;
                                return new KnownPeriodCaregiverSalaryCalculator(caregiverDto);
                            default:
                                throw new NotImplementedException();
                        }
                    }
                case Periods.Active:
                    {
                        switch (salaryCalculatorDto.SalaryCalculatorType)
                        {
                            case SalaryCalculatorTypes.Claimant:
                                var claimantDto = (ActivePeriodClaimantSalaryCalculatorDto)salaryCalculatorDto;
                                return new ActivePeriodClaimantSalaryCalculator(_periodDatesSpecifierFactory, claimantDto);
                            case SalaryCalculatorTypes.Caregiver:
                                var caregiverDto = (ActivePeriodCaregiverSalaryCalculatorDto)salaryCalculatorDto;
                                return new ActivePeriodCaregiverSalaryCalculator(_periodDatesSpecifierFactory, caregiverDto);
                            default:
                                throw new NotImplementedException();
                        }
                    }
                case Periods.Passive:
                    {
                        switch (salaryCalculatorDto.SalaryCalculatorType)
                        {
                            case SalaryCalculatorTypes.Claimant:
                                var claimantDto = (PassivePeriodClaimantSalaryCalculatorDto)salaryCalculatorDto;
                                return new PassivePeriodClaimantSalaryCalculator(claimantDto);
                            case SalaryCalculatorTypes.Caregiver:
                                var caregiverDto = (PassivePeriodCaregiverSalaryCalculatorDto)salaryCalculatorDto;
                                return new PassivePeriodCaregiverSalaryCalculator(caregiverDto);
                            default:
                                throw new NotImplementedException();
                        }
                    }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
