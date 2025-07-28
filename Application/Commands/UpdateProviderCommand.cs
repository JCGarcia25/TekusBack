using Application.DTO;
using MediatR;

namespace Application.Commands
{
    public record UpdateProviderCommand(
    Guid Id,
    string Name,
    string Email,
    List<ServiceDto> Services,
    List<ProviderAttributeDto> Attributes
) : IRequest<bool>;
}
