using AM.Core.Domain.Entities;
using AM.Core.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace AM.Infra.EFCore.Persistence
{
    public class AppDbContextBase : DbContext, IDomainEventContext, IDbFacadeResolver
    {
        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public IEnumerable<EventBase> GetDomainEvents()
        {
            var domainEntities = ChangeTracker
                .Entries<EntityRootBase>()
                .Where(x =>
                    x.Entity.DomainEvents != null &&
                    x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

            return domainEvents;
        }
    }
}