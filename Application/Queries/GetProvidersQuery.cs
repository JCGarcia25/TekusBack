using Domain.Providers;
using MediatR;

namespace Application.Queries
{
    public record GetProvidersQuery : IRequest<IEnumerable<Provider>>;
}
