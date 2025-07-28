using Domain.Shared;
using System.Text.Json.Serialization;

namespace Domain.Services
{
    public class ServiceCountry : BaseEntity
    {
        public Guid ServiceId { get; private set; }
        public string CountryCode { get; private set; } = default!;

        [JsonIgnore]
        public Service? Service { get; private set; }

        public static ServiceCountry Create(Guid serviceId, string countryCode)
            => new() { ServiceId = serviceId, CountryCode = countryCode.ToUpperInvariant() };
    }
}
