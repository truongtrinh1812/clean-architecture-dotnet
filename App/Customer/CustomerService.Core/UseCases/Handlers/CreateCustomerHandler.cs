using AM.Core.Domain.CQRS.Models;
using AM.Core.Repositories;
using AppContracts.Dtos;
using CustomerService.Core.UseCases.Commands;
using CustomerService.Core.Core.Entities;
using MediatR;
using AppContracts.RestApi;
using CustomerService.Core.Core.Specs;

namespace CustomerService.Core.UseCases.Handlers
{
    internal class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ResultModel<CustomerDto>>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICountryApi _countryApi;

        public CreateCustomerHandler(IRepository<Customer> customerRepository, ICountryApi countryApi)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _countryApi = countryApi ?? throw new ArgumentNullException(nameof(countryApi));
        }

        public async Task<ResultModel<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var alreadyRegisteredSpec = new CustomerAlreadyRegisteredSpec(request.Model.Email);

            var existingCustomer = await _customerRepository.FindOneAsync(alreadyRegisteredSpec);

            if (existingCustomer != null)
            {
                throw new Exception("Customer with this email already exists");
            }

            // check country is exists and valid
            var (countryDto, isError, _) = await _countryApi.GetCountryByIdAsync(request.Model.CountryId);
            if (isError || countryDto.Id.Equals(Guid.Empty))
            {
                throw new Exception("Country Id is not valid.");
            }

            var customer = Customer.Create(request.Model.FirstName, request.Model.LastName, request.Model.Email, request.Model.CountryId);

            var created = await _customerRepository.AddAsync(customer);

            return ResultModel<CustomerDto>.Create(new CustomerDto
            {
                Id = created.Id,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email,
                CountryId = created.CountryId,
                Balance = created.Balance,
                Created = created.Created,
                Updated = created.Updated
            });
        }
    }
}