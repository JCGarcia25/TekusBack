using Domain.Countries;

namespace Domain.Interfaces
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country?> GetByCodeAsync(string countryCode);
    }
}
