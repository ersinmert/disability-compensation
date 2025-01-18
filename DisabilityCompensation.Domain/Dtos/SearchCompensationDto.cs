using DisabilityCompensation.Domain.ValueObjects;

namespace DisabilityCompensation.Domain.Dtos
{
    public class SearchCompensationDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public DateTime? Date { get; set; }
        public CompensationStatus? Status { get; set; }
    }
}
