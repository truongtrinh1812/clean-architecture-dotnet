using CustomerService.Core.UseCases.Commands;
using FluentValidation;

namespace CustomerService.Core.UseCases.Validators
{
    internal class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(v => v.Model.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

            RuleFor(v => v.Model.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

            RuleFor(v => v.Model.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email should in email format.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");
        }
    }
}