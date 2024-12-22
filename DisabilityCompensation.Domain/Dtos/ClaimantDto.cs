using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Application.Dtos.Entity
{
    public class ClaimantDto : BaseDto
    {
        public Guid CompensationId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string? TCKN { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? MilitaryStatus { get; set; }
        public string? FatherName { get; set; }
        public string? EmploymentStatus { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public bool? IsMinimumWage { get; set; }
    }
}
