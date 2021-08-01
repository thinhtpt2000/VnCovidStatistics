using AutoMapper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Crawler.Contants;
using VnCovidStatistics.Crawler.Interfaces;

namespace VnCovidStatistics.Crawler.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly ICityService _cityService;
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;

        public CrawlerService(ICityService cityService, IStatisticService statisticService, IMapper mapper)
        {
            _cityService = cityService;
            _statisticService = statisticService;
            _mapper = mapper;
        }

        public async Task<List<StatisticCrawlerDto>> ReadDataFromWebsite()
        {
            var results = new List<StatisticCrawlerDto>();
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(SourceContant.SOURCE_URL);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var table = htmlDocument.DocumentNode.Descendants(SourceContant.TABLE_NAME)
                .FirstOrDefault(node => SourceContant.TABLE_ID.Equals(node.Id));
            var tBody = table?.ChildNodes.FirstOrDefault(
                node => SourceContant.TBODY_NAME.Equals(node.Name));
            if (tBody != null)
            {
                var listRows = tBody.ChildNodes.Where(node => SourceContant.TR_NAME.Equals(node.Name)).ToList();
                var lastUpdated = DateTime.Now;
                foreach (var row in listRows)
                {
                   var listCols = row.ChildNodes.Where(node => SourceContant.TD_NAME.Equals(node.Name)).ToList();
                    if (listCols.Count != 4)
                    {
                        continue;
                    }
                    var data = new StatisticCrawlerDto
                    {
                        CityName = listCols[0].InnerText,
                        TotalCases = Convert.ToInt32(RemoveSymbolsInText(listCols[1].InnerText)),
                        TodayCases = Convert.ToInt32(RemoveSymbolsInText(listCols[2].InnerText)),
                        TotalDeaths = Convert.ToInt32(RemoveSymbolsInText(listCols[3].InnerText)),
                        LastUpdated = lastUpdated
                    };
                    results.Add(data);
                }

            }
            return results;
        }

        public List<StatisticDto> ProcessData(List<StatisticCrawlerDto> sources)
        {
            if (sources == null || sources.Count == 0)
            {
                return null;
            }
            var results = new List<StatisticDto>();
            foreach (var data in sources)
            {
                var city = _cityService.GetCityByName(data.CityName);
                if (city != null)
                {
                    var dto = _mapper.Map<StatisticDto>(data);
                    dto.CityId = city.Id;
                    results.Add(dto);
                }
            }
            return results;
        }

        public async Task WriteData(List<StatisticDto> statisticDtos)
        {
            if (statisticDtos == null || statisticDtos.Count == 0)
            {
                return;
            }
            var statistics = _mapper.Map<List<Statistic>>(statisticDtos);
            await _statisticService.InsertOrUpdateStatistics(statistics);
        }

        private static string RemoveSymbolsInText(string source)
        {
            return string.IsNullOrEmpty(source)
                ? string.Empty
                : string.Join("", source.Split('+', ',', '.'));
        }
    }
}
