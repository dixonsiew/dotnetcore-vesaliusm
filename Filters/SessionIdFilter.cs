using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using vesalius_m.Services;

namespace vesalius_m.Filters
{
    public class SessionIdFilter : IAsyncAuthorizationFilter
    {
        private readonly ApplicationUserService applicationUserService;

        public SessionIdFilter(ApplicationUserService aus)
        {
            applicationUserService = aus;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userx = context.HttpContext.User;
            if (!userx.Identity?.IsAuthenticated ?? true)
            {
                context.Result = Unauthorized("Unauthorized");
                return;
            }

            var userId = userx.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = Unauthorized("Unauthorized");
                return;
            }

            var id = Convert.ToInt64(userId);
            var user = await applicationUserService.FindByUserIdAsync(id);
            if (user == null)
            {
                context.Result = Unauthorized("Unauthorized");
                return;
            }

            var sessionId = userx.FindFirstValue("sessionId");
            var type = userx.FindFirstValue("type");
            if (type != "1")
            {
                context.Result = Unauthorized("Unauthorized");
                return;
            }

            if (string.IsNullOrEmpty(user.SessionId))
            {
                context.Result = Unauthorized("The system has detected your account is no longer valid. Please sign in again.");
                return;
            }

            if (!string.IsNullOrEmpty(sessionId))
            {
                var userSession = await applicationUserService.FindByUserIdSessionId(user.UserId, sessionId);
                if (userSession == null)
                {
                    context.Result = Unauthorized("The system has detected you have signed in using another device. Please sign in again.");
                    return;
                }
            }
        }

        private JsonResult Unauthorized(string ms)
        {
            return new JsonResult(new
            {
                statusCode = StatusCodes.Status401Unauthorized,
                message = ms,
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized,
            };
        }
    }

    public class SessionIdAttribute : TypeFilterAttribute
    {
        public SessionIdAttribute() : base(typeof(SessionIdFilter))
        {
        }
    }
}
