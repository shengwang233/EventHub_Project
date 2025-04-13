using EventHub_Api.DTOs;
using EventHub_Api.Models;
using EventHub_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EventHub_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserProfileService _userProfileService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            AuthService authService,
            UserProfileService userProfileService,
            ILogger<AccountController> logger)
        {
            _authService = authService;
            _userProfileService = userProfileService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var loginResult = await _authService.LoginAsync(dto);
            if (!loginResult.Success)
                return Unauthorized(loginResult.Message);

            Response.Cookies.Append("access_token", loginResult.Token!, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = loginResult.Expiration
            });

            _logger.LogInformation("✅ Logged in user: {Email} | {UserType}", loginResult.Email, loginResult.UserType);

            return Ok(new
            {
                loginResult.Message,
                loginResult.Email,
                loginResult.FirstName,
                loginResult.LastName,
                loginResult.UserType
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            _logger.LogInformation("User signed out");
            return Ok(new { message = "Logout successful" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var user = await _userProfileService.GetCurrentUserAsync(User);
            if (user == null) return Unauthorized();

            return Ok(new
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.UserType
            });
        }
    }
}