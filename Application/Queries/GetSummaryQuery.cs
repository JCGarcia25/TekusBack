using Application.DTO;
using MediatR;

namespace Application.Queries
{
    public record GetSummaryQuery : IRequest<SummaryDto>;


}
