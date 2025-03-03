using DisabilityCompensation.Domain.Dtos.SalaryCalculator;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator.SalaryCalculator;
using DisabilityCompensation.Shared.Constants;
using DisabilityCompensation.Shared.Utilities;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.SalaryCalculator
{
    public class KnownPeriodClaimantSalaryCalculator : ISalaryCalculator
    {
        private readonly KnownPeriodClaimantSalaryCalculatorDto _calculatorDto;

        public KnownPeriodClaimantSalaryCalculator(KnownPeriodClaimantSalaryCalculatorDto calculatorDto)
        {
            _calculatorDto = calculatorDto;
        }

        public decimal Calculate()
        {
            decimal salary = 0;
            if (_calculatorDto.Compensation!.Claimant!.IsMinimumWage != true)
            {
                salary = _calculatorDto.Compensation!.Claimant.MonthlyIncome!.Value;
                return salary;
            }

            var age = DateHelper.CalculateAge(_calculatorDto.Compensation!.Claimant.BirthDate, _calculatorDto.DateRange!.StartDate);
            var isUnder16 = age < 16;
            if (isUnder16)
            {
                salary = GetUnder16Salary();
                if (salary > 0) return salary;
            }
            var hasChildren = _calculatorDto.Compensation!.Claimant.NumberOfChildren > 0;
            if (hasChildren)
            {
                salary = GetChildrenSalary();
                if (salary > 0) return salary;
            }
            salary = _calculatorDto.MinimumWages!.Where(x =>
                                        x.StartDate <= _calculatorDto.DateRange!.StartDate && _calculatorDto.DateRange!.StartDate < x.EndDate
                                        &&
                                        x.MaritalStatus == _calculatorDto.Compensation!.Claimant.MaritalStatus
                                     ).FirstOrDefault()!.NetWage;

            return salary;
        }

        private decimal GetUnder16Salary()
        {
            decimal salary = 0;
            var hasMatchUnder16Dates = DateHelper.IsBetween(_calculatorDto.DateRange!.StartDate, DateRanges.Under16DateRange.Item1, DateRanges.Under16DateRange.Item2);
            if (hasMatchUnder16Dates)
            {
                salary = _calculatorDto.MinimumWages!.Where(x =>
                                                x.StartDate <= _calculatorDto.DateRange!.StartDate && _calculatorDto.DateRange!.StartDate < x.EndDate
                                                &&
                                                x.IsUnder16 == true
                                             ).FirstOrDefault()!.NetWage;
            }
            else
            {
                salary = _calculatorDto.MinimumWages!.Where(x =>
                                                x.StartDate <= _calculatorDto.DateRange!.StartDate && _calculatorDto.DateRange!.StartDate < x.EndDate
                                                &&
                                                x.MaritalStatus == _calculatorDto.Compensation!.Claimant!.MaritalStatus
                                             ).FirstOrDefault()!.NetWage;
            }
            return salary;
        }

        private decimal GetChildrenSalary()
        {
            decimal salary = 0;
            var hasMatchFourChildrenDates = DateHelper.IsBetween(_calculatorDto.DateRange!.StartDate, DateRanges.FourChildrenDateRange.Item1, DateRanges.FourChildrenDateRange.Item2);
            var hasMatchThreeChildrenDates = DateHelper.IsBetween(_calculatorDto.DateRange!.StartDate, DateRanges.ThreeChildrenDateRange.Item1, DateRanges.ThreeChildrenDateRange.Item2);
            if (hasMatchFourChildrenDates && _calculatorDto.Compensation!.Claimant!.NumberOfChildren >= 4)
            {
                salary = _calculatorDto.MinimumWages!.Where(x =>
                                                x.StartDate <= _calculatorDto.DateRange!.StartDate && _calculatorDto.DateRange!.StartDate < x.EndDate
                                                &&
                                                x.NumberOfChildren == 4
                                             ).FirstOrDefault()!.NetWage;
            }
            else if (hasMatchThreeChildrenDates)
            {
                salary = _calculatorDto.MinimumWages!.Where(x =>
                                                x.StartDate <= _calculatorDto.DateRange!.StartDate && _calculatorDto.DateRange!.StartDate < x.EndDate
                                                &&
                                                x.NumberOfChildren == _calculatorDto.Compensation!.Claimant!.NumberOfChildren
                                             ).FirstOrDefault()!.NetWage;
            }
            else
            {
                salary = _calculatorDto.MinimumWages!.Where(x =>
                                                x.StartDate <= _calculatorDto.DateRange!.StartDate && _calculatorDto.DateRange!.StartDate < x.EndDate
                                                &&
                                                x.MaritalStatus == _calculatorDto.Compensation!.Claimant!.MaritalStatus
                                             ).FirstOrDefault()!.NetWage;
            }
            return salary;
        }
    }
}
