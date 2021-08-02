using AutoMapper;
using VnCovidStatistics.Core.DTOs;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<Statistic, StatisticDto>();
            CreateMap<StatisticDto, Statistic>();
            CreateMap<StatisticCrawlerDto, StatisticDto>().ReverseMap();
            CreateMap<StatisticResponseDto, Statistic>();
            CreateMap<Statistic, StatisticResponseDto>()
                .ForMember(des => des.CityName, 
                    options 
                                    => options.MapFrom(src => src.City.CityName));
        }
    }
}