using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.ExtendsCrawler.Constants;
using VnCovidStatistics.ExtendsCrawler.Interfaces;

namespace VnCovidStatistics.ExtendsCrawler.Services
{
    public class CrawlerService : ICrawlerService
    {
        public async Task<List<StatisticCrawlerDto>> ReadDataFromWebsite()
        {
            var results = new List<StatisticCrawlerDto>();
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(SourceConstant.SOURCE_URL);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var table = htmlDocument.DocumentNode.Descendants(SourceConstant.TABLE_NAME)
                .FirstOrDefault(node => SourceConstant.TABLE_ID.Equals(node.Id));
            var tBody = table?.ChildNodes.FirstOrDefault(
                node => SourceConstant.TBODY_NAME.Equals(node.Name));
            if (tBody == null) return results;
            {
                var listRows = tBody.ChildNodes.Where(node => SourceConstant.TR_NAME.Equals(node.Name)).ToList();
                var lastUpdated = DateTime.Now;
                results.AddRange(from row in listRows
                    select row.ChildNodes.Where(node => SourceConstant.TD_NAME.Equals(node.Name)).ToList()
                    into listCols
                    where listCols.Count == 4
                    select new StatisticCrawlerDto
                    {
                        CityName = listCols[0].InnerText,
                        TotalCases = Convert.ToInt32(RemoveSymbolsInText(listCols[1].InnerText)),
                        TodayCases = Convert.ToInt32(RemoveSymbolsInText(listCols[2].InnerText)),
                        TotalDeaths = Convert.ToInt32(RemoveSymbolsInText(listCols[3].InnerText)),
                        LastUpdated = lastUpdated
                    });
            }
            return results;
        }
        
        private static string RemoveSymbolsInText(string source)
        {
            return string.IsNullOrEmpty(source)
                ? string.Empty
                : string.Join("", source.Split('+', ',', '.'));
        }
    }
}
