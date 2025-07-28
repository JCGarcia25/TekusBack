using Domain.Interfaces;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TekusDbContext _context;
        private readonly IProviderRepository _providerRepository;
        private readonly IServiceRepository _serviceRepository;

        public UnitOfWork(TekusDbContext context, IProviderRepository providerRepository, IServiceRepository serviceRepository)
        {
            _context = context;
            _providerRepository = providerRepository;
            _serviceRepository = serviceRepository;
        }
        public IProviderRepository Providers => _providerRepository;
        public IServiceRepository Services => _serviceRepository;
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
