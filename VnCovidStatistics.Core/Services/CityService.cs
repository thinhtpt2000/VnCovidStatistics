using System.Linq;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Core.Options;
using VnCovidStatistics.Core.QueryFilters;
using Microsoft.Extensions.Options;
using VnCovidStatistics.Core.CustomEntities;

namespace VnCovidStatistics.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CityService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<City> GetCities(PageFilter filters)
        {
            // Do paging
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var cities = _unitOfWork.CityRepository.GetAll().OrderBy(x => x.CityName);
            var pagedCities = PagedList<City>.Create(cities, filters.PageNumber, filters.PageSize);
            return pagedCities;
        }

        public City GetCityByName(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return null;
            return _unitOfWork.CityRepository.GetCityByName(cityName);
        }
    }
}
