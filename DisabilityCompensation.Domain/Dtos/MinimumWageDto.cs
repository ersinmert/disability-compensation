using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Domain.Dtos
{
    public class MinimumWageDto : BaseDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal GrossWage { get; set; }
        public decimal NetWage { get; set; }
        public string? MaritalStatus { get; set; }
        public bool? IsUnder16 { get; set; }
        public int? NumberOfChildren { get; set; }
    }
}
