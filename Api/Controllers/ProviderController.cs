using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("with-details")]
        public async Task<IActionResult> CreateProviderWithDetails(CreateProviderWithDetailsCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetProvider), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider(CreateProviderCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProvider), new { id }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            var providers = await _mediator.Send(new GetProvidersQuery());
            return Ok(providers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvider(Guid id)
        {
            var provider = await _mediator.Send(new GetProviderByIdQuery(id));
            if (provider == null)
            {
                return NotFound();
            }
            return Ok(provider);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(Guid id)
        {
            var command = new DeleteProviderCommand(id);
            var result = await _mediator.Send(command);
            if (result == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
