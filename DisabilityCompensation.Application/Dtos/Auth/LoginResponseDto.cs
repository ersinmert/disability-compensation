using DisabilityCompensation.Domain.Dtos;

namespace DisabilityCompensation.Application.Dtos.Auth
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        public List<string>? Authorities { get; set; }
    }
}
