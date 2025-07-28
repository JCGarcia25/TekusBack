using Domain.Shared;
using System.Text.Json.Serialization;

namespace Domain.Providers
{
    public class ProviderAttribute : BaseEntity
    {
        public Guid ProviderId { get; private set; }
        public string Key { get; private set; } = default!;
        public string Value { get; private set; } = default!;
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Provider? Provider { get; private set; }

        public static ProviderAttribute Create(Guid providerId, string key, string value)
            => new() { ProviderId = providerId, Key = key, Value = value };

        public void SoftDelete()
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
