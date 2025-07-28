using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtProvider _jwtProvider;

        public AuthController(JwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Simulación de un usuario previamente creado
            const string predefinedUsername = "admin";
            const string predefinedPassword = "password123";
            var predefinedUserId = Guid.NewGuid();

            if (request.Username == predefinedUsername && request.Password == predefinedPassword)
            {
                var token = _jwtProvider.GenerateToken(predefinedUsername, predefinedUserId, TimeSpan.FromHours(1));
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }

    public record LoginRequest(string Username, string Password);
}
