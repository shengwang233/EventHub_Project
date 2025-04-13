namespace EventHub_Api.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string UserType { get; set; } = "member"; // member/host/admin
    }
}
