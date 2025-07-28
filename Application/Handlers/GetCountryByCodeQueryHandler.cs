using Application.Queries;
using Domain.Countries;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetCountryByCodeQueryHandler : IRequestHandler<GetCountryByCodeQuery, Country?>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountryByCodeQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country?> Handle(GetCountryByCodeQuery request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByCodeAsync(request.Code);

            if (country == null)
                return null;

            return country;
        }
    }
}
