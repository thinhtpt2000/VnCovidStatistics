using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Interfaces;

namespace VnCovidStatistics.API.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _sattisticService;
        private readonly IMapper _mapper;

        public StatisticController(IStatisticService statisticService, IMapper mapper)
        {
            _sattisticService = statisticService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatisticByCityIdAndDate(Guid cityId, DateTime date)
        {
            var statistic = await _sattisticService.GetStatisticByCityAndDate(cityId, date);
            var statisticDto = _mapper.Map<StatisticDto>(statistic);
            return Ok(statisticDto);
        }
    }
}
