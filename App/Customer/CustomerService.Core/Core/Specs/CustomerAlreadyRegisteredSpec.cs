using System.Linq.Expressions;
using AM.Core.Specification;
using CustomerService.Core.Core.Entities;

namespace CustomerService.Core.Core.Specs
{
    public class CustomerAlreadyRegisteredSpec : SpecificationBase<Customer>
    {
        private readonly string _email;

        public CustomerAlreadyRegisteredSpec(string email)
        {
            _email = email;
        }

        public override Expression<Func<Customer, bool>> Criteria => customer => customer.Email == _email;
    }
}