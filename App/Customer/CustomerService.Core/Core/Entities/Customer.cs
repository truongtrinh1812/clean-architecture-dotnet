using System.Collections.ObjectModel;
using AM.Core.Domain.Entities;
using IntegrationEvents.Customer;
using CustomerService.Core.Core.Specs;

namespace CustomerService.Core.Core.Entities
{
    public class Customer : EntityRootBase
    {
        private readonly List<CreditCard> _creditCards = new();

        public string FirstName { get; protected set; } = default!;
        public string LastName { get; protected set; } = default!;
        public string Email { get; protected set; } = default!;
        public decimal Balance { get; protected set; }
        public Guid CountryId { get; protected set; }

        public virtual IEnumerable<CreditCard> CreditCards { get { return _creditCards.AsReadOnly(); } }

        public virtual void ChangeEmail(string email)
        {
            if (Email != email)
            {
                Email = email;
            }
        }

        public static Customer Create(string firstname, string lastname, string email, Guid countryId)
        {
            return Create(Guid.NewGuid(), firstname, lastname, email, countryId);
        }

        public static Customer Create(Guid id, string firstname, string lastname, string email, Guid countryId)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstname");

            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException("lastname");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            if (countryId == null)
                throw new ArgumentNullException("country");

            Customer customer = new()
            {
                Id = id,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                CountryId = countryId
            };

            customer.AddDomainEvent(new CustomerCreatedIntegrationEvent());

            return customer;
        }

        public virtual ReadOnlyCollection<CreditCard> GetCreditCardsAvailable()
        {
            return _creditCards.FindAll(new CreditCardAvailableSpec(DateTime.Today).IsSatisfiedBy).AsReadOnly();
        }

        public virtual void Add(CreditCard creditCard)
        {
            _creditCards.Add(creditCard);
        }
    }
}