using Application.DTO;
using MediatR;

namespace Application.Commands
{
    public record CreateProviderWithDetailsCommand(
    string Nit,
    string Name,
    string Email,
    List<ServiceDto> Services,
    List<ProviderAttributeDto> Attributes
) : IRequest<Guid>;
}
