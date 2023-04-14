using FluentValidation;
using ProductService.Core.UseCases.Queries;

namespace ProductService.Core.UseCases.Validators
{
    internal class GetProductsValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize should at least greater than or equal to 1.");
        }
    }
}