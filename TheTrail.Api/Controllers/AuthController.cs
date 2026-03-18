using Microsoft.AspNetCore.Mvc;
using TheTrail.Services.Core.DTOs.Auth;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AuthResponseDto response = await _authService.RegisterAsync(dto);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto?>> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthResponseDto? response = await _authService.LoginAsync(dto);

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(response);
        }
    }
}