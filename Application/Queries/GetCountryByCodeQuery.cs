using Domain.Countries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public record GetCountryByCodeQuery(string Code) : IRequest<Country?>;
}
