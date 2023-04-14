using FluentValidation;
using ProductService.Core.UseCases.Commands;

namespace ProductService.Core.UseCases.Validators
{
    internal class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(v => v.Model.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

            RuleFor(v => v.Model.ProductCodeName)
                .NotEmpty().WithMessage("ProductCodeName is required.")
                .MaximumLength(5).WithMessage("ProductCodeName must not exceed 5 characters.");

            RuleFor(x => x.Model.Quantity)
                .GreaterThanOrEqualTo(1).WithMessage("Quantity should at least greater than or equal to 1.");

            RuleFor(x => x.Model.Cost)
                .GreaterThanOrEqualTo(1000).WithMessage("Cost should be greater than 1000.");
        }
    }
}