using MediatR;

namespace Application.Commands
{
    public record CreateProviderCommand(string Nit, string Name, string Email) : IRequest<Guid>;
}
