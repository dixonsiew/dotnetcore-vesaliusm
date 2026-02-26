using Dapper;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class AdminUserService
    {
        private readonly DefaultConnection ctx;
        private readonly ILogger<AdminUserService> logger;

        public AdminUserService(DefaultConnection c, ILogger<AdminUserService> log)
        {
            ctx = c;
            logger = log;
        }

        public async Task<AdminUser?> FindByAdminIdAsync(int adminId)
        {
            AdminUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM ADMIN_USER WHERE ADMIN_ID = :adminId", new { adminId });
                if (q != null)
                {
                    o = AdminUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding admin user by adminId: {adminId}", adminId);
                throw;
            }
        }

        public async Task<AdminUser?> FindByUsernameAsync(string username)
        {
            AdminUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM ADMIN_USER WHERE USERNAME = :username", new { username });
                if (q != null)
                {
                    o = AdminUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding admin user by username: {username}", username);
                throw;
            }
        }

        public async Task<AdminUser?> FindByEmailAsync(string email)
        {
            AdminUser? o = null;
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QuerySingleOrDefaultAsync(@"SELECT * FROM ADMIN_USER WHERE EMAIL = :email", new { email });
                if (q != null)
                {
                    o = AdminUser.FromRs(q);
                }

                return o;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding admin user by email: {email}", email);
                throw;
            }
        }
    }
}
