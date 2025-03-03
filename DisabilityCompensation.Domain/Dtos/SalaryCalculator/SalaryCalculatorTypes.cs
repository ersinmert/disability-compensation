using System.ComponentModel;

namespace DisabilityCompensation.Domain.Dtos.SalaryCalculator
{
    public enum SalaryCalculatorTypes
    {
        None = 0,

        [Description("Hak Sahibi")]
        Claimant = 1,

        [Description("Bakıcı")]
        Caregiver = 2
    }
}
