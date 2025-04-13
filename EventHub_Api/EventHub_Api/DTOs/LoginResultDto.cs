namespace EventHub_Api.DTOs
{
    public class LoginResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }

        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserType { get; set; }
    }
}
