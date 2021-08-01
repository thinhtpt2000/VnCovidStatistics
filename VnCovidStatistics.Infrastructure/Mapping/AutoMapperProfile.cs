﻿using AutoMapper;
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
        }
    }
}