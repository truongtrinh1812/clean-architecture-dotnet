using AM.Core.Domain.CQRS.Models;
using AM.Core.Repositories;
using AppContracts.Dtos;
using MediatR;
using SettingService.Core.Core.Entities;
using SettingService.Core.UseCases.Queries;

namespace SettingService.Core.UseCases.Handlers
{
    internal class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, ResultModel<CountryDto>>
    {
        private readonly IRepository<Country> _countryRepository;

        public GetCountryByIdHandler(IRepository<Country> countryRepository)
        {
            _countryRepository =
                countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        public Task<ResultModel<CountryDto>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var country = _countryRepository.FindById(request.Id);

            return Task.FromResult(ResultModel<CountryDto>.Create(new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                Created = country.Created,
                Updated = country.Updated
            }));
        }
    }
}