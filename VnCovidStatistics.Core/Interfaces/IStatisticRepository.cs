using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface IStatisticRepository : IRepository<Statistic>
    {
        Task<Statistic> GetStatisticByCityAndDate(Guid cityId, DateTime date);
    }
}
