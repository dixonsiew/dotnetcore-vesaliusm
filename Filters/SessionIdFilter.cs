using Microsoft.AspNetCore.Authorization;
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
                context.Result = new JsonResult(new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "Unauthorized",
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            var userId = userx.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new JsonResult(new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "Unauthorized",
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            var id = Convert.ToInt64(userId);
            var user = await applicationUserService.FindByUserIdAsync(id);
            if (user == null)
            {
                context.Result = new JsonResult(new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "Unauthorized",
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            var sessionId = userx.FindFirstValue("sessionId");
            var type = userx.FindFirstValue("type");
            if (type != "1")
            {
                context.Result = new JsonResult(new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "Unauthorized",
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            if (string.IsNullOrEmpty(user.SessionId))
            {
                context.Result = new JsonResult(new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "The system has detected your account is no longer valid. Please sign in again.",
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            if (!string.IsNullOrEmpty(sessionId))
            {
                var userSession = await applicationUserService.FindByUserIdSessionId(user.UserId, sessionId);
                if (userSession == null)
                {
                    context.Result = new JsonResult(new
                    {
                        statusCode = StatusCodes.Status401Unauthorized,
                        message = "The system has detected you have signed in using another device. Please sign in again.",
                    })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                    };
                }
            }
        }
    }
}
