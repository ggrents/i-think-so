namespace i_think_so.Application.Models.Request
{
    public class RegisterRequest
    {
        public required string Username { get; set; } 
        public string? Email { get; set; }
        public required string Password { get; set;}
}
}
