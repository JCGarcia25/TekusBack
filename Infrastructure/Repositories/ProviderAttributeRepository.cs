using Domain.Interfaces;
using Domain.Providers;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProviderAttributeRepository : IProviderAttributeRepository
    {
        private readonly TekusDbContext _context;

        public ProviderAttributeRepository(TekusDbContext context)
        {
            _context = context;
        }

        public async Task<ProviderAttribute?> GetByIdAsync(Guid id)
        {
            return await _context.ProviderAttributes.FindAsync(id);
        }

        public async Task<IEnumerable<ProviderAttribute>> GetAllByProviderIdAsync(Guid providerId)
        {
            return await _context.ProviderAttributes
                .Where(pa => pa.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProviderAttribute>> GetAllAsync()
        {
            return await _context.ProviderAttributes.ToListAsync();
        }

        public async Task AddAsync(ProviderAttribute attribute)
        {
            await _context.ProviderAttributes.AddAsync(attribute);
        }

        public void Update(ProviderAttribute attribute)
        {
            _context.ProviderAttributes.Update(attribute);
        }

        public void Delete(ProviderAttribute attribute)
        {
            attribute.SoftDelete();
            _context.ProviderAttributes.Update(attribute);
        }

        public async Task<ProviderAttribute?> GetByKeyAndProviderIdAsync(string key, Guid providerId)
        {
            return await _context.ProviderAttributes
                .FirstOrDefaultAsync(pa => pa.Key == key && pa.ProviderId == providerId);
        }
    }
}
