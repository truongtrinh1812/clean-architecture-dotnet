using AM.Core.Domain.Entities;
using IntegrationEvents.Product;

namespace ProductService.Core.Core.Entities
{
    public class ProductCode : EntityRootBase
    {
        public string Name { get; private init; } = default!;

        public static ProductCode Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static ProductCode Create(Guid id, string name)
        {
            ProductCode productCode = new() { Id = id, Name = name };

            productCode.AddDomainEvent(new ProductCodeCreatedIntegrationEvent
            {
                ProductCodeId = productCode.Id,
                ProductCodeName = productCode.Name
            });

            return productCode;
        }
    }
}