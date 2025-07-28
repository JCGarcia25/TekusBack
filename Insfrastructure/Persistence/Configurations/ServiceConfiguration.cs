using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insfrastructure.Persistence.Configurations
{
    public sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(s => s.HourlyRate)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
