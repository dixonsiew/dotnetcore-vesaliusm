using Dapper;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class AdminUserService
    {
        private readonly DefaultConnection ctx;

        public AdminUserService(DefaultConnection c)
        {
            ctx = c;
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

            catch (Exception)
            {
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

            catch (Exception)
            {
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

            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidateCredentials(AdminUser user, string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, user.Password);
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
