using Oracle.ManagedDataAccess.Client;
using Dapper;
using vesalius_m.Models;
using System.Data;

namespace vesalius_m.Services
{
    public class CountryService
    {
        private readonly IDbConnection conn;

        public CountryService(IDbConnection con)
        {
            conn = con;
        }

        public async Task<List<CountryTelCode>> FindAllCountryTelCodeAsync()
        {
            List<CountryTelCode> lx = new List<CountryTelCode>();
            var q = await conn.QueryAsync(@"SELECT COUNTRY_NAME, TEL_CODE FROM NOVA_COUNTRY WHERE TEL_CODE IS NOT NULL ORDER BY COUNTRY_NAME");
            lx = CountryTelCode.GetQ(q).ToList();
            return lx;
        }

        public async Task<string> FindCountryCodeByNationalityAsync(string nationality)
        {
            string s = "";
            var prm = new { nationality = nationality };
            var q = await conn.ExecuteScalarAsync<string>(@"SELECT COUNTRY_CODE FROM NOVA_COUNTRY WHERE NATIONALITY = :nationality");
            s = q ?? "";
            return s;
        }

        public async Task<List<Country>> FindAllCountriesAsync()
        {
            List<Country> lx = new List<Country>();
            var q = await conn.QueryAsync(@"SELECT COUNTRY_NAME, TEL_CODE, COUNTRY_CODE FROM NOVA_COUNTRY WHERE COUNTRY_NAME IS NOT NULL ORDER BY COUNTRY_NAME");
            lx = Country.GetQ(q).ToList();
            return lx;
        }

        public async Task<List<Nationality>> FindAllNationalitiesAsync()
        {
            List<Nationality> lx = new List<Nationality>();
            var q = await conn.QueryAsync(@"SELECT NATIONALITY FROM NOVA_COUNTRY WHERE NATIONALITY IS NOT NULL ORDER BY NATIONALITY");
            lx = Nationality.GetQ(q).ToList();
            return lx;
        }
    }
}
