using Doccure.IdentityService.Dtos;
using Doccure.IdentityService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateRegister(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result)
            {
                return BadRequest("Registration failed.");
            }
            return Ok("Registration Successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> CreateLogin(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null)
            {
                return BadRequest("Login failed.");
            }
            return Ok(new
            {
                message = "Giriş başarılı",
                token
            });
        }
    }
}
