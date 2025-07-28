using Domain.Providers;
using MediatR;

namespace Application.Queries
{
    public record GetProviderByIdQuery(Guid Id) : IRequest<Provider?>;
}
