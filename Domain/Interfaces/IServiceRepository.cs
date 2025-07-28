using Domain.Services;

namespace Domain.Interfaces
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task AddAsync(Service service);
        void Update(Service service);
        void Delete(Service service);

    }
}
