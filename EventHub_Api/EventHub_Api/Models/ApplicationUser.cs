using Microsoft.AspNetCore.Identity;

namespace EventHub_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserType { get; set; } = "member"; // member / host / admin
    }
}
