using System;
using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        
        IRepository<City> CityRepository { get; }
        
        IRepository<Statistic> StatisticRepository { get; }
        
        void SaveChanges();

        Task SaveChangesAsync();
    }
}