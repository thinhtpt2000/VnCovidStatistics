using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface IStatisticService
    {
        IEnumerable<Statistic> GetAll();
        Task<Statistic> GetStatisticByCityAndDate(Guid cityId, DateTime date);
        Task InsertOrUpdateStatistics(List<Statistic> statistics);
    }
}
