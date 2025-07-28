using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _mediator.Send(new GetCountriesQuery());
            return Ok(countries);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCountryByCode(string code)
        {
            var country = await _mediator.Send(new GetCountryByCodeQuery(code));

            if (country == null)
                return NotFound();

            return Ok(country);
        }
    }
}
