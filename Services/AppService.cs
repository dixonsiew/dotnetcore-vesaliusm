using Dapper;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class AppService
    {
        private readonly DefaultConnection ctx;

        public AppService(DefaultConnection c)
        {
            ctx = c;
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

            catch (Exception)
            {
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

            catch (Exception)
            {
                throw;
            }
        }
    }
}
