using Dapper;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class AppService
    {
        private readonly DefaultConnection ctx;
        private readonly ILogger<AppService> logger;

        public AppService(DefaultConnection c, ILogger<AppService> log)
        {
            ctx = c;
            logger = log;
        }

        public async Task<List<HospitalProfile>> FindAllAppHospitalProfileAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT * FROM HOSPITAL_PROFILE");
                var lx = HospitalProfile.List(q);
                return lx;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding all hospital profiles");
                throw;
            }
        }

        public async Task<List<AppVersion>> FindAllAppVersionAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT * FROM APP_VERSION");
                var lx = AppVersion.List(q);
                return lx;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding all app versions");
                throw;
            }
        }
    }
}
