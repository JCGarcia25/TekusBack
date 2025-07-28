using Application.DTO;
using Application.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class GetSummaryQueryHandler : IRequestHandler<GetSummaryQuery, SummaryDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSummaryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SummaryDto> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
        {
            var providers = await _unitOfWork.Providers.GetAllAsync();
            var providersByCountry = providers
                .SelectMany(p => p.Services.SelectMany(s => s.Countries))
                .GroupBy(c => c.CountryCode)
                .ToDictionary(g => g.Key, g => g.Count());

            var services = await _unitOfWork.Services.GetAllAsync();
            var servicesByCountry = services
                .SelectMany(s => s.Countries)
                .GroupBy(c => c.CountryCode)
                .ToDictionary(g => g.Key, g => g.Count());

            return new SummaryDto(providersByCountry, servicesByCountry);
        }
    }
}
