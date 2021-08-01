using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Services;
using VnCovidStatistics.Crawler.Services;
using VnCovidStatistics.Infrastructure.Data;
using VnCovidStatistics.Infrastructure.Repositories;

namespace VnCovidStatistics.Crawler
{
    class Program
    {
        static async Task Main()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StatisticCrawlerDto, StatisticDto>();
                cfg.CreateMap<StatisticDto, StatisticCrawlerDto>();
                cfg.CreateMap<Statistic, StatisticDto>();
                cfg.CreateMap<StatisticDto, Statistic>();
            });
            var mapper = config.CreateMapper();
            var context = new VietnamCovidStatisticsContext();
            var unitOfWork = new UnitOfWork(context);
            var cityService = new CityService(unitOfWork);
            var statisticService = new StatisticService(unitOfWork);
            var crawlerService = new CrawlerService(cityService, statisticService, mapper);
            List<StatisticCrawlerDto> statisticsFromWeb = await crawlerService.ReadDataFromWebsite();
            List<StatisticDto> statisticDtos = crawlerService.ProcessData(statisticsFromWeb);
            await crawlerService.WriteData(statisticDtos);
        }
    }
}
