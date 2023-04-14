using FluentValidation;
using ProductService.Core.UseCases.Queries;

namespace ProductService.Core.UseCases.Validators
{
    internal class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}