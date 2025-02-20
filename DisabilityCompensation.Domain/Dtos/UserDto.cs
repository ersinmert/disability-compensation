using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Domain.Dtos
{
    public class UserDto : BaseDto
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
    }
}
