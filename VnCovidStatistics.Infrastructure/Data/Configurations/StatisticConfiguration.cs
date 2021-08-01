using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Infrastructure.Data.Configurations
{
    class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {

            builder.ToTable("Statistics");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                    .HasDefaultValueSql("(newid())")
                    .HasColumnName("StatisticsId"); ;

            builder.Property(e => e.LastUpdated).HasColumnType("datetime");

            builder.HasOne(d => d.City)
                .WithMany(p => p.Statistics)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Statistics_City");
        }
    }
}
