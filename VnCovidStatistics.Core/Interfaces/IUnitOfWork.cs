using System;
using System.Threading.Tasks;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        
        ICityRepository CityRepository { get; }

        IStatisticRepository StatisticRepository { get; }
        
        void SaveChanges();

        Task SaveChangesAsync();
    }
}