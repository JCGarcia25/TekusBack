using Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProviderAttributeRepository
    {
        Task<ProviderAttribute?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProviderAttribute>> GetAllByProviderIdAsync(Guid providerId);
        Task<IEnumerable<ProviderAttribute>> GetAllAsync();
        Task AddAsync(ProviderAttribute attribute);
        void Update(ProviderAttribute attribute);
        void Delete(ProviderAttribute attribute);
        Task<ProviderAttribute?> GetByKeyAndProviderIdAsync(string key, Guid providerId);
    }
}
