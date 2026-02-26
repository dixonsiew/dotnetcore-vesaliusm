using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using vesalius_m.Dtos;
using vesalius_m.Services;

namespace vesalius_m.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AdminUserService adminUserService;
        private readonly AuthService authService;
        private readonly TokenService tokenService;
        private readonly TokenAdminService tokenAdminService;
        private readonly ILogger<AuthController> logger;

        public AuthController(
            AdminUserService aus, 
            AuthService asv, 
            TokenService ts, 
            TokenAdminService tas, 
            ILogger<AuthController> log)
        {
            adminUserService = aus;
            authService = asv;
            tokenService = ts;
            tokenAdminService = tas;
            logger = log;
        }

        private void HandleError(Exception ex)
        {
            if (ex is BadHttpRequestException || ex is UnauthorizedAccessException)
            { }

            else
            {
                logger.LogError(ex, ex.StackTrace);
            }
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginDto data)
        {
            try
            {
                if (data.FromAdmin)
                {
                    var user = await adminUserService.FindByEmailAsync(data.Username) ?? throw new UnauthorizedAccessException("Invalid Credentials");
                    bool valid = adminUserService.ValidateCredentials(user, data.Password);
                    if (!valid)
                    {
                        throw new UnauthorizedAccessException("Invalid Credentials");
                    }

                    var token = tokenAdminService.GenerateAccessToken(user);
                    var refreshToken = tokenAdminService.GenerateRefreshToken(user);

                    Response.Headers.Append(HeaderNames.Authorization, token);
                    return Results.Json(new
                    {
                        type = "bearer",
                        token,
                        refresh_token = refreshToken,
                        isFirstTimeLogin = false,
                        role = user.Role,
                    });
                }

                else
                {
                    var user = await authService.AuthenticateUser(data) ?? throw new UnauthorizedAccessException("Invalid Credentials");
                    var token = tokenService.GenerateAccessToken(user);
                    var refreshToken = tokenService.GenerateRefreshToken(user);

                    Response.Headers.Append(HeaderNames.Authorization, token);
                    return Results.Json(new
                    {
                        type = "bearer",
                        token,
                        refresh_token = refreshToken,
                        isFirstTimeLogin = user.FirstTimeLogin,
                        isFirstTimeBiometric = user.FirstTimeBiometric,
                        role = user.Role,
                    });
                }
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }
    }
}
