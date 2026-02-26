using Microsoft.AspNetCore.Mvc;
using vesalius_m.Models;
using vesalius_m.Services;

namespace vesalius_m.Controllers
{
    [Route("common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly CountryService countryService;
        private readonly AppService appService;
        private readonly ILogger<CommonController> logger;

        public CommonController(ILogger<CommonController> log, CountryService cs, AppService asv)
        {
            countryService = cs;
            appService = asv;
            logger = log;
        }

        private void HandleError(Exception ex)
        {
            if (ex is BadHttpRequestException)
            { }

            else
            {
                logger.LogError(ex, ex.StackTrace);
            }
        }

        [HttpGet("app/hospital-profile")]
        public async Task<List<HospitalProfile>> GetAppHospitalProfile()
        {
            try
            {
                return await appService.FindAllAppHospitalProfileAsync();
            }
            
            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }

        [HttpGet("app/version")]
        public async Task<List<AppVersion>> GetAppVersion()
        {
            try
            {
                return await appService.FindAllAppVersionAsync();
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }

        [HttpGet("telcode/list")]
        public async Task<List<CountryTelCode>> GetCountriesTelCode()
        {
            try
            {
                return await countryService.FindAllCountryTelCodeAsync();
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }

        [HttpGet("country/list")]
        public async Task<List<Country>> GetCountriesAsync()
        {
            try
            {
                return await countryService.FindAllCountriesAsync();
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }

        [HttpGet("nationality/list")]
        public async Task<List<Nationality>> GetNationalities()
        {
            try
            {
                return await countryService.FindAllNationalitiesAsync();
            }

            catch (Exception ex)
            {
                HandleError(ex);
                throw;
            }
        }
    }
}
