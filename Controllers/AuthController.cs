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
        private readonly AuthService authService;
        private readonly TokenService tokenService;
        private readonly ILogger<AuthController> logger;

        public AuthController(AuthService asv, TokenService ts, ILogger<AuthController> log)
        {
            authService = asv;
            tokenService = ts;
            logger = log;
        }

        private void handleError(Exception ex)
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

                }

                else
                {
                    var user = await authService.AuthenticateUser(data);
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
                handleError(ex);
                throw;
            }

            throw new UnauthorizedAccessException("Invalid Credentials");
        }
    }
}
