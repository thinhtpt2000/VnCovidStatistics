using System.Collections.Generic;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface ICityService
    {
        IEnumerable<City> GetCities();

        City GetCityByName(string cityName);
    }
}
