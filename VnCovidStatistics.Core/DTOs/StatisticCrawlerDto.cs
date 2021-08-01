using System;

namespace VnCovidStatistics.Core.DTOs
{
    public class StatisticCrawlerDto
    {
        public int TotalDeaths { get; set; }
        public int TotalCases { get; set; }
        public int TodayCases { get; set; }
        public DateTime LastUpdated { get; set; }
        public string CityName { get; set; }
    }
}
