// AuthService.cs
using EventHub_Api.DTOs;
using EventHub_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EventHub_Api.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtService jwtService,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            var newUser = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserType = dto.UserType.ToLower()
            };

            return await _userManager.CreateAsync(newUser, dto.Password);
        }

       public async Task<LoginResultDto> LoginAsync(LoginDto dto)
{
    var user = await _userManager.FindByEmailAsync(dto.Email);
    if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
    {
        return new LoginResultDto
        {
            Success = false,
            Message = "Invalid credentials"
        };
    }

    var token = _jwtService.GenerateJwtToken(user);
    return new LoginResultDto
    {
        Success = true,
        Message = "Login successful",
        Token = token,
        Expiration = DateTime.UtcNow.AddMinutes(60),
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        UserType = user.UserType
    };
}
    }
}