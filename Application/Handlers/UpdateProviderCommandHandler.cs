using Application.Commands;
using Domain.Interfaces;
using Domain.Providers;
using Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(request.Id);
            if (provider == null)
            {
                throw new Exception($"Provider with ID {request.Id} not found.");
            }

            provider.UpdateDetails(request.Name, request.Email);

            foreach (var service in provider.Services.ToList())
            {
                service.SoftDelete();
            }
            foreach (var serviceDto in request.Services)
            {
                var service = Service.Create(provider.Id, serviceDto.Name, serviceDto.HourlyRate);
                foreach (var countryCode in serviceDto.CountryCodes)
                {
                    var serviceCountry = ServiceCountry.Create(service.Id, countryCode);
                    service.Countries.Add(serviceCountry);
                }
                await _unitOfWork.Services.AddAsync(service);
            }
            foreach (var attribute in provider.CustomAttributes.ToList())
            {
                attribute.SoftDelete();
            }
            foreach (var attributeDto in request.Attributes)
            {
                var attribute = ProviderAttribute.Create(provider.Id, attributeDto.Key, attributeDto.Value);
                await _unitOfWork.ProviderAttributes.AddAsync(attribute);
            }
            _unitOfWork.Providers.Update(provider);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
