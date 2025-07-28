using Domain.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nit)
                   .HasMaxLength(20)
                   .IsRequired();
            builder.Property(p => p.Name)
                   .HasMaxLength(250)
                   .IsRequired();
            builder.Property(p => p.Email)
                   .HasMaxLength(320)
                   .IsRequired();


            // Soft-delete global filter
            builder.HasQueryFilter(p => !p.IsDeleted);

            // Relations
            builder.HasMany(p => p.Services)
                   .WithOne(s => s.Provider)
                   .HasForeignKey(s => s.ProviderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
