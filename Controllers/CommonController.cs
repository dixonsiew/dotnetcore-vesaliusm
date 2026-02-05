using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using vesalius_m.Models;
using vesalius_m.Services;

namespace vesalius_m.Controllers
{
    [Route("common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private CountryService cs;
        private AppService asv;

        public CommonController(IDbConnection con)
        {
            cs = new CountryService(con);
            asv = new AppService(con);
        }

        [HttpGet("app/version")]
        public async Task<List<AppVersion>> GetAppVersion()
        {
            return await asv.FindAllAppVersionAsync();
        }

        [HttpGet("telcode/list")]
        public async Task<List<CountryTelCode>> GetCountriesTelCode()
        {
            return await cs.FindAllCountryTelCodeAsync();
        }

        [HttpGet("country/list")]
        public async Task<List<Country>> GetCountriesAsync()
        {
            return await cs.FindAllCountriesAsync();
        }

        [HttpGet("nationality/list")]
        public async Task<List<Nationality>> GetNationalities()
        {
            return await cs.FindAllNationalitiesAsync();
        }
    }
}
