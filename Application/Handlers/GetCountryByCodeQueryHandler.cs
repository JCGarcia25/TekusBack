using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
