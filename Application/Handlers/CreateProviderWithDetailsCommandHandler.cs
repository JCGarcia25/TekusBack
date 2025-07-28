using Application.Commands;
using Domain.Interfaces;
using Domain.Providers;
using Domain.Services;
using MediatR;

namespace Application.Handlers
{
    public class CreateProviderWithDetailsCommandHandler : IRequestHandler<CreateProviderWithDetailsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProviderWithDetailsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProviderWithDetailsCommand request, CancellationToken cancellationToken)
        {
            var existingProvider = await _unitOfWork.Providers.GetByNitAsync(request.Nit);
            if (existingProvider != null)
            {
                throw new Exception($"Provider with NIT {request.Nit} already exists.");
            }

            var provider = Provider.Create(request.Nit, request.Name, request.Email);

            foreach (var serviceDto in request.Services)
            {
                var service = Service.Create(provider.Id, serviceDto.Name, serviceDto.HourlyRate);
                foreach (var countryCode in serviceDto.CountryCodes)
                {
                    var serviceCountry = ServiceCountry.Create(service.Id, countryCode);
                    service.Countries.Add(serviceCountry);
                }
                provider.Services.Add(service);
            }

            foreach (var attributeDto in request.Attributes)
            {
                var attribute = ProviderAttribute.Create(provider.Id, attributeDto.Key, attributeDto.Value);
                provider.CustomAttributes.Add(attribute);
            }

            await _unitOfWork.Providers.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return provider.Id;
        }
    }
}
