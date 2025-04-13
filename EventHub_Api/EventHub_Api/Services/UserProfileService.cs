using EventHub_Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EventHub_Api.Services
{
    public class UserProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync(ClaimsPrincipal userClaims)
        {
            var userId = userClaims.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId)) return null;

            return await _userManager.FindByIdAsync(userId);
        }
    }
}
