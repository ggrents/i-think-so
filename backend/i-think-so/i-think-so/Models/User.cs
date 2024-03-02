using MongoDB.Bson;

namespace i_think_so.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
