using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;

namespace VnCovidStatistics.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<City> GetCities()
        {
            var cities = _unitOfWork.CityRepository.GetAll().OrderBy(x => x.CityName);
            return cities;
        }

        public City GetCityByName(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return null;
            return _unitOfWork.CityRepository.GetCityByName(cityName);
        }
    }
}
