using Swashbuckle.AspNetCore.Annotations;

namespace DisabilityCompensation.Shared.Dtos
{
    public class UserClaim
    {
        [SwaggerIgnore]
        public Guid UserId { get; set; }
    }
}
