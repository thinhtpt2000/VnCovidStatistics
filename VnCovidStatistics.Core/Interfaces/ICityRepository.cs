using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        City GetCityByName(string cityName);
    }
}
