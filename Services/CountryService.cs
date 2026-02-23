using Dapper;
using System.Data;
using vesalius_m.Models;
using vesalius_m.Utils;

namespace vesalius_m.Services
{
    public class CountryService
    {
        private readonly DefaultConnection ctx;
        private readonly ILogger<CountryService> logger;

        public CountryService(DefaultConnection c, ILogger<CountryService> log)
        {
            ctx = c;
            logger = log;
        }

        public async Task<List<CountryTelCode>> FindAllCountryTelCodeAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT COUNTRY_NAME, TEL_CODE FROM NOVA_COUNTRY WHERE TEL_CODE IS NOT NULL ORDER BY COUNTRY_NAME");
                var lx = CountryTelCode.List(q);
                return lx;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding all country tel codes");
                throw;
            }
        }

        public async Task<string> FindCountryCodeByNationalityAsync(string nationality)
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var prm = new { nationality };
                var q = await conn.ExecuteScalarAsync<string>(@"SELECT COUNTRY_CODE FROM NOVA_COUNTRY WHERE NATIONALITY = :nationality");
                var s = q ?? "";
                return s;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding all country codes");
                throw;
            }
        }

        public async Task<List<Country>> FindAllCountriesAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT COUNTRY_NAME, TEL_CODE, COUNTRY_CODE FROM NOVA_COUNTRY WHERE COUNTRY_NAME IS NOT NULL ORDER BY COUNTRY_NAME");
                var lx = Country.List(q);
                return lx;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding all countries");
                throw;
            }
        }

        public async Task<List<Nationality>> FindAllNationalitiesAsync()
        {
            try
            {
                using var conn = ctx.CreateConnection();
                var q = await conn.QueryAsync(@"SELECT NATIONALITY FROM NOVA_COUNTRY WHERE NATIONALITY IS NOT NULL ORDER BY NATIONALITY");
                var lx = Nationality.List(q);
                return lx;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error finding all nationalities");
                throw;
            }
        }
    }
}
