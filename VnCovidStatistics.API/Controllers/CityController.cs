using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VnCovidStatistics.API.Responses;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Core.QueryFilters;

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
        public IActionResult GetCities([FromQuery] PageFilter filters)
        {
            var cities = _cityService.GetCities(filters);
            var citesDto = _mapper.Map<IEnumerable<CityDto>>(cities);
            var metaData = new MetaData
            {
                TotalCount = cities.TotalCount,
                PageSize = cities.PageSize,
                CurrentPage = cities.CurrentPage,
                TotalPages = cities.TotalPages,
                HasNextPage = cities.HasNextPage,
                HasPreviousPage = cities.HasPreviousPage,
            };
            var response = new ApiResponse<IEnumerable<CityDto>>(citesDto)
            {
                meta = metaData
            };
            return Ok(response);
        }
    }
}