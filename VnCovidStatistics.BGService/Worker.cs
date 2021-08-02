using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.ExtendsCrawler.Interfaces;

namespace VnCovidStatistics.BGService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            using (var scope = _serviceProvider.CreateScope())
            {
                var dataFromWebsite = await scope.ServiceProvider.GetRequiredService<ICrawlerService>().ReadDataFromWebsite();
                var listDataDto = ProcessData(dataFromWebsite, scope.ServiceProvider.GetRequiredService<ICityService>());
                await WriteData(listDataDto, scope.ServiceProvider.GetRequiredService<IStatisticService>());
            }
            _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
        }

        private List<StatisticDto> ProcessData(IReadOnlyCollection<StatisticCrawlerDto> sources, ICityService cityService)
        {
            if (sources == null || sources.Count == 0)
            {
                return null;
            }
            var results = new List<StatisticDto>();
            foreach (var data in sources)
            {
                var city = cityService.GetCityByName(data.CityName);
                if (city == null) continue;
                var dto = _mapper.Map<StatisticDto>(data);
                dto.CityId = city.Id;
                results.Add(dto);
            }
        
            return results;
        }
        
        private async Task WriteData(ICollection statisticDto, IStatisticService statisticService)
        {
            if (statisticDto == null || statisticDto.Count == 0)
            {
                return;
            }
            var statistics = _mapper.Map<List<Statistic>>(statisticDto);
            await statisticService.InsertOrUpdateStatistics(statistics);
        }
    }
}