using AM.Core.Specification;
using CustomerService.Core.Core.Entities;
using System.Linq.Expressions;

namespace CustomerService.Core.Core.Specs
{
    public class CreditCardAvailableSpec : SpecificationBase<CreditCard>
    {
        private readonly DateTime _dateTime;

        public CreditCardAvailableSpec(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public override Expression<Func<CreditCard, bool>> Criteria => creditCard => creditCard.Active && creditCard.Expiry >= _dateTime;
    }
}