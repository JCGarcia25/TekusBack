
namespace Application.DTO
{
    public record SummaryDto(
        Dictionary<string, int> ProvidersByCountry,
        Dictionary<string, int> ServicesByCountry
    );
}
