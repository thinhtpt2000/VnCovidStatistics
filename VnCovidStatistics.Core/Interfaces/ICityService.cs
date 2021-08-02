using VnCovidStatistics.Core.CustomEntities;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.QueryFilters;

namespace VnCovidStatistics.Core.Interfaces
{
    public interface ICityService
    {
        PagedList<City> GetCities(PageFilter filters);

        City GetCityByName(string cityName);
    }
}
