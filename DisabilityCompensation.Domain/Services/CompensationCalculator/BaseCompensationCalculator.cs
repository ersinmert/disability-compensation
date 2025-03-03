namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public abstract class BaseCompensationCalculator
    {
        public decimal CalculateCompensation(decimal dailySalary, int totalDays, decimal disabilityRate, decimal faultRate)
        {
            return dailySalary * totalDays * disabilityRate * faultRate;
        }
    }
}
