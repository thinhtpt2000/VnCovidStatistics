﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using VnCovidStatistics.Infrastructure.Data;

namespace VnCovidStatistics.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VietnamCovidStatisticsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("VnCovidDb"))
            );
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VnCovidStatistics.API", Version = "v1" });
            });
            return services;
        }
    }
}
