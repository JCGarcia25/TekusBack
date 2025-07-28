using Application.Queries;
using Domain.Interfaces;
using Domain.Providers;
using MediatR;

namespace Application.Handlers
{
    public class GetProviderByIdQueryHandler : IRequestHandler<GetProviderByIdQuery, Provider?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProviderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Provider?> Handle(GetProviderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Providers.GetByIdAsync(request.Id);
        }
    }
}
