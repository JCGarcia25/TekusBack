using Domain.Countries;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<Country>? _cachedCountries;

        // Inyección de dependencias
        public CountryRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            if (_cachedCountries != null)
                return _cachedCountries;

            var response = await _httpClient.GetAsync("https://api.first.org/data/v1/countries?limit=249");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var countriesResponse = JsonSerializer.Deserialize<CountriesApiResponse>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (countriesResponse?.Data == null)
                return Enumerable.Empty<Country>();

            // Mapear DTO a objetos de dominio
            var countries = countriesResponse.Data.Select(kvp =>
                Country.Create(
                    kvp.Key,
                    kvp.Value.Country,
                    kvp.Value.Region ?? "Unknown"
                )).ToList();

            _cachedCountries = countries;
            return countries;
        }

        public async Task<Country?> GetByCodeAsync(string countryCode)
        {
            var countries = await GetAllAsync();
            return countries.FirstOrDefault(c =>
                c.Code.Equals(countryCode, StringComparison.OrdinalIgnoreCase));
        }
    }

    // DTOs para deserialización (clases internas al repositorio)
    internal class CountriesApiResponse
    {
        public Dictionary<string, CountryDto> Data { get; set; } = new();
    }

    internal class CountryDto
    {
        public string Country { get; set; } = string.Empty;
        public string? Region { get; set; }
    }
}
