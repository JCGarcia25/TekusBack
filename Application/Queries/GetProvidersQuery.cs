using Application.Common;
using Domain.Providers;
using MediatR;

namespace Application.Queries
{
    public record GetProvidersQuery : PaginationParameters, IRequest<PaginatedList<Provider>>;
}
