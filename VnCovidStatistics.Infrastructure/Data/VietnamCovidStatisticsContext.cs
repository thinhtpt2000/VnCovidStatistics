using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VnCovidStatistics.Core.Entities;

#nullable disable

namespace VnCovidStatistics.Infrastructure.Data
{
    public partial class VietnamCovidStatisticsContext : DbContext
    {
        public VietnamCovidStatisticsContext()
        {
        }

        public VietnamCovidStatisticsContext(DbContextOptions<VietnamCovidStatisticsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=THINHTPT-PC\\SQLEXPRESS;Database=VietnamCovidStatistics;Integrated Security = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
