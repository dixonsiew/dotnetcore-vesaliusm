using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using vesalius_m.Services;

namespace vesalius_m.Policies
{
    public class SessionIdRequirement : IAuthorizationRequirement { }

    public class SessionIdHandler : AuthorizationHandler<SessionIdRequirement>
    {
        private readonly ApplicationUserService applicationUserService;

        public SessionIdHandler(ApplicationUserService aus)
        {
            applicationUserService = aus;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, SessionIdRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                context.Fail(new AuthorizationFailureReason(this, "Unauthorized"));
                return Task.CompletedTask; // No user ID, can't authorize
            }

            var id = Convert.ToInt64(userId);
            var user = await applicationUserService.FindByUserIdAsync(id);
            if (user == null)
            {
                context.Fail(new AuthorizationFailureReason(this, "Unauthorized"));
                return Task.CompletedTask;
            }

            var sessionId = context.User.FindFirstValue("sessionId");
            var type = context.User.FindFirstValue("type");
            if (type != "1")
            {
                context.Fail(new AuthorizationFailureReason(this, "Unauthorized"));
                return Task.CompletedTask;
            }
            
            if (string.IsNullOrEmpty(user.SessionId))
            {
                context.Fail(new AuthorizationFailureReason(this, "The system has detected your account is no longer valid. Please sign in again."));
                return Task.CompletedTask;
            }

            if (!string.IsNullOrEmpty(sessionId))
            {
                var userSession = await applicationUserService.FindByUserIdSessionId(user.UserId, sessionId);
                if (userSession == null)
                {
                    context.Fail(new AuthorizationFailureReason(this, "The system has detected you have signed in using another device. Please sign in again."));
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
