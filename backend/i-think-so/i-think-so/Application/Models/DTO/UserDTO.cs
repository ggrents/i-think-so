namespace i_think_so.Application.Models.DTO
{
    public class UserDTO
    {
        public required string Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
    }
}
