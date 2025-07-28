using Domain.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insfrastructure.Persistence.Configurations
{
    public sealed class ProviderAttributeConfiguration : IEntityTypeConfiguration<ProviderAttribute>
    {
        public void Configure(EntityTypeBuilder<ProviderAttribute> builder)
        {
            builder.ToTable("ProviderAttributes");
            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Key)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(pa => pa.Value)
                   .HasMaxLength(500)
                   .IsRequired();
            builder.Property(pa => pa.CreatedAt)
                   .IsRequired();

            builder.Property(pa => pa.DeletedAt)
                   .IsRequired(false);

            builder.Property(pa => pa.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            // Soft-delete global filter
            builder.HasQueryFilter(pa => !pa.IsDeleted);
        }
    }
}
