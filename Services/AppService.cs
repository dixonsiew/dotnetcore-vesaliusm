using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task<List<AppServices>> FindAllGuestModeAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT * FROM APP_SERVICE WHERE IS_GUEST_ONLY = 1 AND IS_ENABLED = 1 ORDER BY SERVICE_DISPLAY_ORDER");
                var lx = AppServices.List(q);
                return lx;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedList<AppServices>> ListByGuestMode()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var total = await CountByGuestModeAsync();
                var lx = await FindAllGuestModeAsync();
                var m = new PagedList<AppServices>
                {
                    List = lx,
                    Total = total,
                    TotalPages = 1,
                };
                return m;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CountByGuestModeAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                int q = await conn.ExecuteScalarAsync<int>(@"SELECT COUNT(SERVICE_NAME) AS COUNT FROM APP_SERVICE WHERE IS_GUEST_ONLY = 1 AND IS_ENABLED = 1");
                return q;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AppServices>> FindAllAuthModeAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT * FROM APP_SERVICE WHERE IS_ENABLED = 1 ORDER BY SERVICE_DISPLAY_ORDER");
                var lx = AppServices.List(q);
                return lx;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedList<AppServices>> ListByAuthMode()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var total = await CountByAuthModeAsync();
                var lx = await FindAllAuthModeAsync();
                var m = new PagedList<AppServices>
                {
                    List = lx,
                    Total = total,
                    TotalPages = 1,
                };
                return m;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CountByAuthModeAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                int q = await conn.ExecuteScalarAsync<int>(@"SELECT COUNT(SERVICE_NAME) AS COUNT FROM APP_SERVICE WHERE IS_ENABLED = 1");
                return q;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
