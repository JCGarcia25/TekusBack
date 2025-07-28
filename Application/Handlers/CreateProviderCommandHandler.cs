using Application.Commands;
using Domain.Interfaces;
using Domain.Providers;
using MediatR;

namespace Application.Handlers
{
    public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateProviderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = Provider.Create(request.Nit, request.Name, request.Email);
            await _unitOfWork.Providers.AddAsync(provider);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return provider.Id;
        }
    }
}
