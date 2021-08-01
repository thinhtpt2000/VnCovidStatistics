using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Interfaces;

namespace VnCovidStatistics.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityService.GetCities();
            var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(cityDtos);
        }
    }
}
