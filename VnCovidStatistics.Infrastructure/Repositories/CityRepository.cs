using System.Linq;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Infrastructure.Data;

namespace VnCovidStatistics.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(VietnamCovidStatisticsContext context) : base(context)
        {
        }

        public City GetCityByName(string cityName)
        {
            return _entities.FirstOrDefault(x => x.CityName == cityName);
        }
    }
}
