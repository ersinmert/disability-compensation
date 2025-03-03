using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Domain.Dtos
{
    public class LifeDto : BaseDto
    {
        public string? LifeType { get; set; }
        public string? Gender { get; set; }
        public int CurrentAge { get; set; }
        public decimal EstimatedYear { get; set; }
    }
}
