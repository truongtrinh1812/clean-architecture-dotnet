using FluentValidation;
using SettingService.Core.UseCases.Queries;

namespace SettingService.Core.UseCases.Validators
{
    internal class GetCountryByIdValidator : AbstractValidator<GetCountryByIdQuery>
    {
        public GetCountryByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}