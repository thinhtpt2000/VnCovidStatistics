using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Infrastructure.Data;

namespace VnCovidStatistics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VietnamCovidStatisticsContext _context;
        private readonly ICityRepository _cityRepository;
        private readonly IStatisticRepository _staticRepository;
        
        public UnitOfWork(VietnamCovidStatisticsContext context)
        {
            this._context = context;
        }

        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);

        public IStatisticRepository StatisticRepository => _staticRepository ?? new StatisticRepository(_context);

        public void Dispose()
        {
            _context?.Dispose();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}