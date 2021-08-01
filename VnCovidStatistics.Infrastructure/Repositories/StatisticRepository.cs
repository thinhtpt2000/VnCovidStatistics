using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Infrastructure.Data;

namespace VnCovidStatistics.Infrastructure.Repositories
{
    public class StatisticRepository : BaseRepository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(VietnamCovidStatisticsContext context) : base(context)
        {
        }

        public async Task<Statistic> GetStatisticByCityAndDate(Guid cityId, DateTime date)
        {
            return await _entities.FirstOrDefaultAsync(x => x.CityId == cityId 
                                                && x.LastUpdated.Date.CompareTo(date.Date) == 0);
        }

        public override IEnumerable<Statistic> GetAll() {
            return _entities.Include(x => x.City);
        }
    }
}
