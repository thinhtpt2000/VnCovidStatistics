using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.CustomEntities;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.QueryFilters;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface IStatisticService
    {
        PagedList<Statistic> GetAll(PageFilter filters);
        Task<Statistic> GetStatisticByCityAndDate(Guid cityId, DateTime date);
        Task InsertOrUpdateStatistics(IEnumerable<Statistic> statistics);
    }
}
