using Domain.Services;
using Domain.Providers;
using Microsoft.EntityFrameworkCore;

namespace Insfrastructure.Persistence
{
    public class TekusDbContext : DbContext
    {
        public TekusDbContext(DbContextOptions<TekusDbContext> options) : base(options) { }

        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<ProviderAttribute> ProviderAttributes => Set<ProviderAttribute>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<ServiceCountry> ServiceCountries => Set<ServiceCountry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TekusDbContext).Assembly);
        }
    }
}
