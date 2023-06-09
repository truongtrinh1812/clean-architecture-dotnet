using AM.Core.Domain.Entities;
using IntegrationEvents.Setting;

namespace SettingService.Core.Core.Entities
{
    public class Country : EntityRootBase
    {
        public string Name { get; protected set; } = default!;

        public static Country Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static Country Create(Guid id, string name)
        {
            Country country = new()
            {
                Id = id,
                Name = name
            };

            country.AddDomainEvent(new CountryCreatedIntegrationEvent { Id = country.Id, Name = country.Name });

            return country;
        }
    }
}