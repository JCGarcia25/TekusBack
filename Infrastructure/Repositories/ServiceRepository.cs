using Domain.Services;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly TekusDbContext _context;
        public ServiceRepository(TekusDbContext context)
        {
            _context = context;
        }
        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services.FindAsync(id);
        }
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services
                .Where(s => !s.IsDeleted && s.Provider != null && !s.Provider.IsDeleted)
                .Include(s => s.Countries)
                .ToListAsync();
        }
        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }
        public void Update(Service service)
        {
            _context.Services.Update(service);
        }
        public void Delete(Service service)
        {
            service.SoftDelete();
            _context.Services.Update(service);
        }
    }
}
