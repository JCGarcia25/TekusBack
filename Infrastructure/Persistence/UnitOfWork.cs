using Domain.Interfaces;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TekusDbContext _context;
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderAttributeRepository _providerAttributeRepository;
        private readonly IServiceRepository _serviceRepository;

        public UnitOfWork(TekusDbContext context, IProviderRepository providerRepository, IServiceRepository serviceRepository, IProviderAttributeRepository providerAttributeRepository)
        {
            _context = context;
            _providerRepository = providerRepository;
            _serviceRepository = serviceRepository;
            _providerAttributeRepository = providerAttributeRepository;
        }
        public IProviderAttributeRepository ProviderAttributes => _providerAttributeRepository;
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
