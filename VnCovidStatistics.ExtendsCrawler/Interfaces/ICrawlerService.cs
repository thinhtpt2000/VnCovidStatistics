using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.DTOs;

namespace VnCovidStatistics.ExtendsCrawler.Interfaces
{
    public interface ICrawlerService
    {
        Task<List<StatisticCrawlerDto>> ReadDataFromWebsite();
    }
}
