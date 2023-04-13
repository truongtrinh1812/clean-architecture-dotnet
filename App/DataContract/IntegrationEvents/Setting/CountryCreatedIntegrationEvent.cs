using AM.Core.Domain.Events;

namespace IntegrationEvents.Setting
{
    public class CountryCreatedIntegrationEvent : EventBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;

        public override void Flatten()
        {
            MetaData.Add("Id", Id);
            MetaData.Add("Name", Name);
        }
    }
}