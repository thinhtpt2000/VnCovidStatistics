using Microsoft.AspNetCore.Mvc;
using VnCovidStatistics.Core.Interfaces;

namespace VnCovidStatistics.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityService.GetCities();
            return Ok(cities);
        }
    }
}
