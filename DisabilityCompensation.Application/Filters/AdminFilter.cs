using DisabilityCompensation.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DisabilityCompensation.Application.Filters
{
    public class AdminFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserService _userService;

        public AdminFilter(IUserService userService)
        {
            _userService = userService;
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

            var isAdmin = await _userService.AnyAsync(x => x.Id == userGuid && x.IsAdmin && x.IsActive);
            if (!isAdmin)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
