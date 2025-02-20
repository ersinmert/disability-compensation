using DisabilityCompensation.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DisabilityCompensation.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserClaim GetClaims(this ClaimsPrincipal? user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new ArgumentNullException(userId);
            }

            return new UserClaim
            {
                UserId = Guid.Parse(userId!)
            };
        }
    }
}
