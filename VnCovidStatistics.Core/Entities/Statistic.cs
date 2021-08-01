using System;
using System.Collections.Generic;

#nullable disable

namespace VnCovidStatistics.Core.Entities
{
    public class Statistic : BaseEntity
    {
        public int TotalDeaths { get; set; }
        public int TotalCases { get; set; }
        public int TodayCases { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
    }
}
