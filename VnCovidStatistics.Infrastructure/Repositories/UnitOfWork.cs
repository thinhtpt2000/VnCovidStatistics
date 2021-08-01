using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Infrastructure.Data;

namespace VnCovidStatistics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VietnamCovidStatisticsContext _context;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Statistic> _staticRepository;
        
        public UnitOfWork(VietnamCovidStatisticsContext context)
        {
            this._context = context;
        }

        public IRepository<City> CityRepository => _cityRepository ?? new BaseRepository<City>(_context);

        public IRepository<Statistic> StatisticRepository => _staticRepository ?? new BaseRepository<Statistic>(_context);

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