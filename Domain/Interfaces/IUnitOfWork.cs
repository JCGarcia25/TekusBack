namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProviderRepository Providers { get; }
        IServiceRepository Services { get; }
        IProviderAttributeRepository ProviderAttributes { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
