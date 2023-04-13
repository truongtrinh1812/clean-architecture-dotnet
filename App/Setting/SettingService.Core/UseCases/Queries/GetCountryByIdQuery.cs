using AM.Core.Domain.CQRS.Queries;
using AppContracts.Dtos;

namespace SettingService.Core.UseCases.Queries
{
    public record GetCountryByIdQuery : IItemQuery<Guid, CountryDto>
    {
        public List<string> Includes { get; init; } = new();
        public Guid Id { get; init; }
    }
}