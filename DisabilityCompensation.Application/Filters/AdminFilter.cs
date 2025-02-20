using DisabilityCompensation.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DisabilityCompensation.Application.Filters
{
    public class AdminFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserRoleService _userRoleService;

        public AdminFilter(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userGuid = Guid.Parse(userId);

            var isAdmin = await _userRoleService.IsAdmin(userGuid);
            if (!isAdmin)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
