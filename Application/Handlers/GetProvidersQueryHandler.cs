using Application.Common;
using Application.Queries;
using Domain.Interfaces;
using Domain.Providers;
using MediatR;
using System.Linq.Expressions;

namespace Application.Handlers
{
    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, PaginatedList<Provider>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProvidersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<Provider>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
        {
            // Obtener la consulta base
            var providers = (await _unitOfWork.Providers.GetAllAsync()).AsQueryable();

            // Aplicar búsqueda si se proporciona un término de búsqueda
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                providers = providers.Where(p =>
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.Nit.ToLower().Contains(searchTerm) ||
                    p.Email.ToLower().Contains(searchTerm));
            }

            // Aplicar ordenamiento
            providers = ApplySorting(providers, request.SortBy, request.SortAscending);


            return PaginatedList<Provider>.Create(
                providers,
                request.PageNumber,
                request.PageSize);
        }

        private IQueryable<Provider> ApplySorting(IQueryable<Provider> query, string? sortBy, bool sortAscending)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                // Ordenamiento predeterminado
                return query.OrderBy(p => p.Name);
            }

            Expression<Func<Provider, object>> keySelector = sortBy.ToLower() switch
            {
                "name" => p => p.Name,
                "nit" => p => p.Nit,
                "email" => p => p.Email,
                "createdat" => p => p.CreatedAt,
                _ => p => p.Name // Default
            };

            return sortAscending
                ? query.OrderBy(keySelector)
                : query.OrderByDescending(keySelector);
        }
    }
}
