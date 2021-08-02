using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;

namespace VnCovidStatistics.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatisticService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Statistic> GetAll()
        {
            return _unitOfWork.StatisticRepository.GetAll();
        }

        public async Task<Statistic> GetStatisticByCityAndDate(Guid cityId, DateTime date)
        {
            return await _unitOfWork.StatisticRepository.GetStatisticByCityAndDate(cityId, date);
        }

        public async Task InsertOrUpdateStatistics(IEnumerable<Statistic> statistics)
        {
            foreach (var statistic in statistics)
            {
                var existingStatistic = await GetStatisticByCityAndDate(statistic.CityId, statistic.LastUpdated);
                if (existingStatistic == null)
                {
                    await _unitOfWork.StatisticRepository.Add(statistic);
                }
                else
                {
                    existingStatistic.TotalCases = statistic.TotalCases;
                    existingStatistic.TodayCases = statistic.TodayCases;
                    existingStatistic.TotalDeaths = statistic.TotalDeaths;
                    existingStatistic.LastUpdated = statistic.LastUpdated;
                    _unitOfWork.StatisticRepository.Update(existingStatistic);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
