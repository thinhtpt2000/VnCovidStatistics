using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VnCovidStatistics.Core.Entities;

namespace VnCovidStatistics.Infrastructure.Data.Configurations
{
    class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                    .HasDefaultValueSql("(newid())")
                    .HasColumnName("CityId"); ;

            builder.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
