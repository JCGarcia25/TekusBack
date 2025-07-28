using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class ServiceCountryConfiguration : IEntityTypeConfiguration<ServiceCountry>
    {
        public void Configure(EntityTypeBuilder<ServiceCountry> builder)
        {
            builder.ToTable("ServiceCountries");
            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.CountryCode)
                   .HasMaxLength(3)
                   .IsFixedLength()
                   .IsRequired();

            builder.HasIndex(sc => new { sc.ServiceId, sc.CountryCode })
                   .IsUnique();

            builder.HasOne(sc => sc.Service)
                   .WithMany(s => s.Countries)
                   .HasForeignKey(sc => sc.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
