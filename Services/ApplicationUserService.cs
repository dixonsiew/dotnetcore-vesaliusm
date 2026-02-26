using Dapper;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class ApplicationUserService
    {
        private readonly DefaultConnection ctx;

        public ApplicationUserService(DefaultConnection c)
        {
            ctx = c;
        }

        public async Task<ApplicationUser?> FindByUserIdAsync(long userId)
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

            catch (Exception)
            {
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

            catch (Exception)
            {
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

            catch (Exception)
            {
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

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> SaveSessionIdAsync(long userId)
        {
            try
            {
                string sid = string.Empty;
                string sessionId = new UUID().ToFormattedString();
                using var conn = ctx.CreateConnection();
                await conn.ExecuteAsync(@"UPDATE APPLICATION_USER SET SESSION_ID = :sessionId WHERE USER_ID = :userId", new { sessionId, userId });
                sid = sessionId;
                return sid;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateMachineIdAsync(string id, long userId)
        {
            try
            {
                string machineId = BCrypt.Net.BCrypt.HashPassword(id);
                using var conn = ctx.CreateConnection();
                await conn.ExecuteAsync(@"UPDATE APPLICATION_USER SET MACHINE_ID = :machineId WHERE USER_ID = :userId", new { machineId, userId });
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePlayerIdAsync(string id, long userId)
        {
            try
            {
                using var conn = ctx.CreateConnection();
                await conn.ExecuteAsync(@"UPDATE APPLICATION_USER SET PLAYER_ID = NULL WHERE PLAYER_ID = :id", new { id });
                await conn.ExecuteAsync(@"UPDATE APPLICATION_USER SET PLAYER_ID = :id WHERE USER_ID = :userId", new { id, userId });
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertDownloadApp(string playerId)
        {
            try
            {
                using var conn = ctx.CreateConnection();
                await conn.ExecuteAsync(@"
                    MERGE INTO APP_DOWNLOADED_USER apu
                    USING (SELECT :playerId AS PLAYER_ID FROM DUAL) src
                    ON (apu.PLAYER_ID = src.PLAYER_ID)
                    WHEN NOT MATCHED THEN
                    INSERT (PLAYER_ID) VALUES (src.PLAYER_ID)", new { playerId });
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertDownloadAppV2(string machineId, string playerId)
        {
            try
            {
                using var conn = ctx.CreateConnection();
                await conn.ExecuteAsync(@"
                    MERGE INTO APP_DOWNLOADED_USER apu
                    USING (SELECT :machineId AS MACHINE_ID, :playerId AS PLAYER_ID FROM DUAL) src
                    ON (apu.MACHINE_ID = src.MACHINE_ID)
                    WHEN MATCHED THEN
                    UPDATE SET apu.PLAYER_ID = src.PLAYER_ID, DATE_UPDATE = CURRENT_TIMESTAMP
                    WHEN NOT MATCHED THEN
                    INSERT (MACHINE_ID, PLAYER_ID, DATE_UPDATE) VALUES (src.MACHINE_ID, src.PLAYER_ID, CURRENT_TIMESTAMP)", new { machineId, playerId });
            }

            catch (Exception)
            {
                throw;
            }
        }

        public bool ValidateCredentials(ApplicationUser user, string password)
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
