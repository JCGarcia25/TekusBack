using Domain.Providers;

namespace Domain.Interfaces
{
    public interface IProviderRepository
    {
        Task<Provider?> GetByIdAsync(Guid id);
        Task<Provider?> GetByNitAsync(string nit);
        Task<IEnumerable<Provider>> GetAllAsync();
        Task AddAsync(Provider provider);
        void Update(Provider provider);
        void Delete(Provider provider);
    }
}
