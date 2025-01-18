using DisabilityCompensation.Application.Attributes;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DisabilityCompensation.Application.Filters
{
    public class AuthorityFilter : IAsyncAuthorizationFilter
    {
        private readonly Authority _authority;
        private readonly IUserAuthorityService _userAuthorityService;

        public AuthorityFilter(Authority authority, IUserAuthorityService userAuthorityService)
        {
            _userAuthorityService = userAuthorityService;
            _authority = authority;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userGuid = Guid.Parse(userId);

            var hasAuthority = await _userAuthorityService.HasAuthorityAsync(userGuid, _authority);
            if (!hasAuthority)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
