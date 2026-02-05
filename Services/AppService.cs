using Dapper;
using System.Data;
using vesalius_m.Models;

namespace vesalius_m.Services
{
    public class AppService
    {
        private readonly IDbConnection conn;

        public AppService(IDbConnection con)
        {
            conn = con;
        }

        public async Task<List<HospitalProfile>> FindAllAppHospitalProfileAsync()
        {
            List<HospitalProfile> lx = new List<HospitalProfile>();
            var q = await conn.QueryAsync(@"SELECT * FROM HOSPITAL_PROFILE");
            lx = HospitalProfile.GetQ(q).ToList();
            return lx;
        }

        public async Task<List<AppVersion>> FindAllAppVersionAsync()
        {
            List<AppVersion> lx = new List<AppVersion>();
            var q = await conn.QueryAsync(@"SELECT * FROM APP_VERSION");
            lx = AppVersion.GetQ(q).ToList();
            return lx;
        }
    }
}
