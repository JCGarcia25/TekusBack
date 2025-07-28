using MediatR;

namespace Application.Handlers
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<Country>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountriesQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetAllAsync();

            return countries;
        }
    }
}
