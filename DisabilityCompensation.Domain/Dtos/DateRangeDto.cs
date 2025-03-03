namespace DisabilityCompensation.Domain.Dtos
{
    public class DateRangeDto
    {
        public DateRangeDto()
        {

        }

        public DateRangeDto(DateOnly startDate, DateOnly endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var dateRange = (DateRangeDto)obj;
            return StartDate == dateRange.StartDate && EndDate == dateRange.EndDate;
        }
    }
}
