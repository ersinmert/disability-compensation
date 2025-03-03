using DisabilityCompensation.Shared.Constants;

namespace DisabilityCompensation.Shared.Utilities
{
    public static class DateHelper
    {
        public static int CalculateAge(DateOnly birthDate, DateOnly date)
        {
            int age = date.Year - birthDate.Year;
            if (date < new DateOnly(date.Year, birthDate.Month, birthDate.Day))
            {
                age--;
            }
            return age;
        }

        public static bool IsBetween(DateOnly date, DateOnly startDate, DateOnly endDate)
        {
            return startDate <= date && date < endDate;
        }

        public static int BetweenTotalDays(DateOnly startDate, DateOnly endDate)
        {
            var totalDays = (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).TotalDays;

            return (int)totalDays;
        }
    }
}
