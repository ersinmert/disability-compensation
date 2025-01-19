using DisabilityCompensation.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DisabilityCompensation.Shared.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserClaim GetClaims(this HttpContext? httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
