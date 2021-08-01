using System;
using System.Collections.Generic;

#nullable disable

namespace VnCovidStatistics.Core.Entities
{
    public class City : BaseEntity
    {
        public City()
        {
            Statistics = new HashSet<Statistic>();
        }
        public string CityName { get; set; }
        public virtual ICollection<Statistic> Statistics { get; set; }
    }
}
