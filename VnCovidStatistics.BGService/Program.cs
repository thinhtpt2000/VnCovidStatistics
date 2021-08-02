using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VnCovidStatistics.ExtendsCrawler.Interfaces;
using VnCovidStatistics.ExtendsCrawler.Services;
using VnCovidStatistics.Infrastructure.Data;
using VnCovidStatistics.Infrastructure.Extension;

namespace VnCovidStatistics.BGService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    services.AddDbContext<VietnamCovidStatisticsContext>(options =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("VnCovidDb"));
                    }, ServiceLifetime.Transient);
                    services.AddTransient<ICrawlerService, CrawlerService>();
                    services.AddServices();
                    services.AddHostedService<Worker>();
                });
    }
}