using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.ValueObjects;
using DisabilityCompensation.Shared.Dtos.Bases;
using DisabilityCompensation.Shared.Utilities;

namespace DisabilityCompensation.Application.Dtos.Entity
{
    public class CompensationDto : BaseDto
    {
        public string? PredefinedNote { get; set; }
        public string? Note { get; set; }
        public CompensationStatus Status { get; set; }
        public bool? HasTemporaryDisability { get; set; }
        public bool? HasCaregiver { get; set; }
        public int? TemporaryDisabilityDay { get; set; }
        public string? RejectReason { get; set; }

        public ClaimantDto? Claimant { get; set; }
        public EventDto? Event { get; set; }
        public UserDto? CreatedByUser { get; set; }
        public List<ExpenseDto>? Expenses { get; set; }
        public List<DocumentDto>? Documents { get; set; }
        public List<CompensationCalculationDto>? CompensationCalculations { get; set; }

        public int GetAgeOnDate(DateOnly date)
        {
            return DateHelper.CalculateAge(Claimant!.BirthDate, date);
        }
    }
}
