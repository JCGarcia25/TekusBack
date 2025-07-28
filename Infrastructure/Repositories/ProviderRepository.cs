using Domain.Interfaces;
using Domain.Providers;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    public class ProviderRepository : IProviderRepository
    {
        private readonly TekusDbContext _context;

        public ProviderRepository(TekusDbContext context)
        {
            _context = context;
        }

        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers
                .Include(p => p.Services)
                .ThenInclude(s => s.Countries)
                .Include(p => p.CustomAttributes)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public Task<Provider?> GetByNitAsync(string nit)
        {
            return _context.Providers
                .Include(p => p.Services)
                .ThenInclude(s => s.Countries)
                .Include(p => p.CustomAttributes)
                .FirstOrDefaultAsync(p => p.Nit == nit && !p.IsDeleted);
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _context.Providers.ToListAsync();
        }

        public async Task AddAsync(Provider provider)
        {
            await _context.Providers.AddAsync(provider);
        }

        public void Update(Provider provider)
        {
            _context.Providers.Update(provider);
        }

        public void Delete(Provider provider)
        {
            provider.SoftDelete();
            _context.Providers.Update(provider);
        }
    }
}
