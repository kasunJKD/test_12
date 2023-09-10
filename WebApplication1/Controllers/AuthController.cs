using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(string username, string password)
        {
            return Ok(await _authService.RegisterUser(username, password));
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(User user)
        {
            if (await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenStringAsync(user);
                return Ok(tokenString);
            }
            return BadRequest();
        }
    }
}
