using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vesalius_m.Filters;
using vesalius_m.Models;
using vesalius_m.Services;
using vesalius_m.Utils;

namespace vesalius_m.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize()]
    [TypeFilter(typeof(SessionIdFilter))]
    public class UserController : ControllerBase
    {
        private readonly ApplicationUserService applicationUserService;
        private readonly ILogger<UserController> logger;

        public UserController(ApplicationUserService aus, ILogger<UserController> log)
        {
            applicationUserService = aus;
            logger = log;
        }

        private void HandleError(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            { }

            else
            {
                logger.LogError(ex, ex.StackTrace);
            }
        }

        [HttpGet("all")]
        public async Task<List<ApplicationUser>> GetAllUsers(
            [FromQuery(Name = "_page")] string _page = "1", 
            [FromQuery(Name = "_limit")] string _limit = "10")
        {
            try
            {
                int page = Convert.ToInt32(_page);
                int limit = Convert.ToInt32(_limit);
                var m = await applicationUserService.ListAsync(page, limit);

                Response.Headers.Append(Constants.X_TOTAL_COUNT, $"{m.Total}");
                Response.Headers.Append(Constants.X_TOTAL_PAGE, $"{m.TotalPages}");
                return m.List;
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }

        [HttpGet("all/active")]
        public async Task<List<ApplicationUser>>  GetAllActiveUsers(
            [FromQuery(Name = "_page")] string _page = "1",
            [FromQuery(Name = "_limit")] string _limit = "10")
        {
            try
            {
                int page = Convert.ToInt32(_page);
                int limit = Convert.ToInt32(_limit);
                var m = await applicationUserService.ListActiveAsync(page, limit);

                Response.Headers.Append(Constants.X_TOTAL_COUNT, $"{m.Total}");
                Response.Headers.Append(Constants.X_TOTAL_PAGE, $"{m.TotalPages}");
                return m.List;
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }
    }
}
