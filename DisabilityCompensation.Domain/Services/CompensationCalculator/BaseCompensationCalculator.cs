namespace DisabilityCompensation.Domain.Services.CompensationCalculator
{
    public abstract class BaseCompensationCalculator
    {
        public decimal CalculateCompensation(decimal dailySalary, int totalDays, decimal disabilityRate, decimal faultRate)
        {
            return Math.Round(dailySalary * totalDays * disabilityRate * faultRate, 2);
        }
    }
}
