using Application.Queries;
using Domain.Interfaces;
using Domain.Providers;
using MediatR;

namespace Application.Handlers
{
    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, IEnumerable<Provider>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProvidersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Provider>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Providers.GetAllAsync();
        }
    }
}
