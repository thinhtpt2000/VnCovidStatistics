using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using VnCovidStatistics.Core.CustomEntities;
using VnCovidStatistics.Core.Entities;
using VnCovidStatistics.Core.Interfaces;
using VnCovidStatistics.Core.Options;
using VnCovidStatistics.Core.QueryFilters;

namespace VnCovidStatistics.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public StatisticService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Statistic> GetAll(PageFilter filters)
        {
            // Do paging
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var statistics = _unitOfWork.StatisticRepository.GetAll();
            var pagedStatistics = PagedList<Statistic>.Create(statistics, filters.PageNumber, filters.PageSize);
            return pagedStatistics;
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
