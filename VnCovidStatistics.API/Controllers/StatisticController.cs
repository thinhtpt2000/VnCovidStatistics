using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VnCovidStatistics.API.Responses;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Interfaces;

namespace VnCovidStatistics.API.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;

        public StatisticController(IStatisticService statisticService, IMapper mapper)
        {
            _statisticService = statisticService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllStatistics()
        {
            var statistic = _statisticService.GetAll();
            var statisticDto = _mapper.Map<IEnumerable<StatisticResponseDto>>(statistic);
            var response = new ApiResponse<IEnumerable<StatisticResponseDto>>(statisticDto);
            return Ok(response);
        }

        [HttpGet]
        [Route("city")]
        public async Task<IActionResult> GetStatisticByCityIdAndDate(Guid cityId, DateTime date)
        {
            var statistic = await _statisticService.GetStatisticByCityAndDate(cityId, date);
            var statisticDto = _mapper.Map<StatisticResponseDto>(statistic);
            var response = new ApiResponse<StatisticResponseDto>(statisticDto);
            return Ok(response);
        }
    }
}
