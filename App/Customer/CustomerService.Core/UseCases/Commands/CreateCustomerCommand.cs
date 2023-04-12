using AM.Core.Domain.CQRS.Commands;
using AppContracts.Dtos;

namespace CustomerService.Core.UseCases.Commands
{
    public record CreateCustomerCommand : ICreateCommand<CreateCustomerCommand.CreateCustomerModel, CustomerDto>
    {
        public CreateCustomerModel Model { get; init; } = default!;
        public record CreateCustomerModel(string FirstName, string LastName, string Email, Guid CountryId);
    }
}