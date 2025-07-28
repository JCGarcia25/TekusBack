using Domain.Shared;
using Domain.Providers;
using System.Text.Json.Serialization;

namespace Domain.Services
{
    public class Service : BaseEntity
    {
        public Guid ProviderId { get; private set; }
        public string Name { get; private set; } = default!;
        public decimal HourlyRate { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Provider? Provider { get; private set; }
        public ICollection<ServiceCountry> Countries { get; private set; } = [];

        public static Service Create(Guid providerId, string name, decimal hourlyRate)
            => new() { ProviderId = providerId, Name = name, HourlyRate = hourlyRate };
        public void SoftDelete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
