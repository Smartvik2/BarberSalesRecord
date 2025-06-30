using BarberSalesRecord.DTOs;
using BarberSalesRecord.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberSalesRecord.Controllers
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
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result.StartsWith("Invalid") || result.StartsWith("Account"))
                return Unauthorized(new { message = result });

            return Ok(new { token = result });
        }
    }
}
