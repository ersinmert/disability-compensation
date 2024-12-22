using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Application.Dtos.Entity
{
    public class ParameterDto : BaseDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
