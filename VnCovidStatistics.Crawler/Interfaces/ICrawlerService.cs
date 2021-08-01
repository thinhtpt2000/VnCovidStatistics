using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnCovidStatistics.Core.DTOs;

namespace VnCovidStatistics.Crawler.Interfaces
{
    public interface ICrawlerService
    {
        Task<List<StatisticCrawlerDto>> ReadDataFromWebsite();
    }
}
