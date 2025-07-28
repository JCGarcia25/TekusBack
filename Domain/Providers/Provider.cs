using Domain.Services;
using Domain.Shared;

namespace Domain.Providers
{
    public class Provider : BaseEntity
    {
        public string Nit { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public ICollection<Service> Services { get; private set; } = [];
        public ICollection<ProviderAttribute> CustomAttributes { get; private set; } = [];

        public static Provider Create(string nit, string name, string email)
            => new() { Nit = nit, Name = name, Email = email };

        public void SoftDelete()
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
        public void UpdateDetails(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
