using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Domain.Services.CompensationCalculator.DateRangeCalculator
{
    public abstract class BaseDateRangeCalculator
    {
        public HashSet<DateRangeDto> GetDateRanges(DateOnly startDate, DateOnly endDate)
        {
            var dataRanges = new HashSet<DateRangeDto>();

            AddFirstYear(startDate, dataRanges);
            AddLastYear(endDate, dataRanges);

            var startYear = startDate.Year + 1;
            var endYear = endDate.Year - 1;
            if (endYear - startYear > 0)
            {
                while (startYear <= endYear)
                {
                    dataRanges.Add(new DateRangeDto
                    {
                        StartDate = new DateOnly(startYear, 1, 1),
                        EndDate = new DateOnly(startYear, 7, 1)
                    });
                    dataRanges.Add(new DateRangeDto
                    {
                        StartDate = new DateOnly(startYear, 7, 1),
                        EndDate = new DateOnly(startYear + 1, 1, 1)
                    });
                    startYear++;
                }
            }

            return dataRanges;
        }

        public void AddTemporaryDisabilityDatesToDateRanges(HashSet<DateRangeDto> dateRanges, DateOnly temporaryDisabilityEndDate)
        {
            var dateRange = dateRanges.FirstOrDefault(x => x.StartDate < temporaryDisabilityEndDate && temporaryDisabilityEndDate < x.EndDate);
            if (dateRange != null)
            {
                dateRanges.Remove(dateRange);
                dateRanges.Add(new DateRangeDto
                {
                    StartDate = dateRange.StartDate,
                    EndDate = temporaryDisabilityEndDate
                });
                dateRanges.Add(new DateRangeDto
                {
                    StartDate = temporaryDisabilityEndDate,
                    EndDate = dateRange.EndDate
                });

            }
        }

        private static void AddLastYear(DateOnly endDate, HashSet<DateRangeDto> dataRanges)
        {
            if (endDate < new DateOnly(endDate.Year, 7, 1))
            {
                dataRanges.Add(new DateRangeDto
                {
                    StartDate = new DateOnly(endDate.Year, 1, 1),
                    EndDate = endDate
                });
            }
            else
            {
                dataRanges.Add(new DateRangeDto
                {
                    StartDate = new DateOnly(endDate.Year, 1, 1),
                    EndDate = new DateOnly(endDate.Year, 7, 1)
                });
                dataRanges.Add(new DateRangeDto
                {
                    StartDate = new DateOnly(endDate.Year, 7, 1),
                    EndDate = endDate
                });
            }
        }

        private static void AddFirstYear(DateOnly startDate, HashSet<DateRangeDto> dataRanges)
        {
            if (startDate > new DateOnly(startDate.Year, 7, 1))
            {
                dataRanges.Add(new DateRangeDto
                {
                    StartDate = startDate,
                    EndDate = new DateOnly(startDate.Year + 1, 1, 1)
                });
            }
            else
            {
                dataRanges.Add(new DateRangeDto
                {
                    StartDate = startDate,
                    EndDate = new DateOnly(startDate.Year, 7, 1)
                });
                dataRanges.Add(new DateRangeDto
                {
                    StartDate = new DateOnly(startDate.Year, 7, 1),
                    EndDate = new DateOnly(startDate.Year + 1, 1, 1)
                });
            }
        }
    }
}
