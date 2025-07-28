using MediatR;

namespace Application.Commands
{
    public record DeleteProviderCommand(Guid Id) : IRequest<Guid>;
}
