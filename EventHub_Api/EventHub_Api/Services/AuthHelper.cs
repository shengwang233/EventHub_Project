using System.Security.Claims;

namespace EventHub_Api.Services
{
    public class AuthHelper
    {
        public static string? GetUserIdFromClaims(ClaimsPrincipal user)
        {
            return user?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        }

        public static bool IsAdmin(ClaimsPrincipal user)
        {
            return user?.IsInRole("admin") ?? false;
        }
    }
}
