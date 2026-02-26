using Dapper;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class ApplicationUserService
    {
        private readonly DefaultConnection ctx;
        private readonly ILogger<ApplicationUserService> logger;

        public ApplicationUserService(DefaultConnection c, ILogger<ApplicationUserService> log)
        {
            ctx = c;
            logger = log;
        }

        public async Task<ApplicationUser?> FindByUserIdAsync(int userId)
        {
            ApplicationUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM APPLICATION_USER WHERE USER_ID = :userId", new { userId });
                if (q != null)
                {
                    o = ApplicationUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding application user by userId: {userId}", userId);
                throw;
            }
        }

        public async Task<ApplicationUser?> FindByUsernameAsync(string username)
        {
            ApplicationUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM APPLICATION_USER WHERE LOWER(USERNAME) = LOWER(:username) ORDER BY REGISTRATION_DATE_TIME DESC", new { username });
                if (q != null)
                {
                    o = ApplicationUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding application user by username: {username}", username);
                throw;
            }
        }

        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            ApplicationUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM APPLICATION_USER WHERE LOWER(EMAIL) = LOWER(:email)", new { email });
                if (q != null)
                {
                    o = ApplicationUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding application user by email: {email}", email);
                throw;
            }
        }

        public async Task<ApplicationUser?> FindByPRNAsync(string prn)
        {
            ApplicationUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM APPLICATION_USER WHERE MASTER_PRN = :prn", new { prn });
                if (q != null)
                {
                    o = ApplicationUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding application user by prn: {prn}", prn);
                throw;
            }
        }

        public bool ValidateCredentials(ApplicationUser user, string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, user.Password);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error validating credentials for user with userId: {UserId}", user.UserId);
            }

            return false;
        }
    }
}
