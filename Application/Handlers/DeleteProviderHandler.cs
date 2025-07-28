using Application.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class DeleteProviderHandler : IRequestHandler<DeleteProviderCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProviderHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(request.Id);
            if (provider == null)
            {
                throw new Exception("Provider not found");
            }
            _unitOfWork.Providers.Delete(provider);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return provider.Id;
        }
    }
}
