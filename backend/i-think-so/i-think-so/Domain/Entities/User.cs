using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace i_think_so.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public required string Username { get; set; }

        public string Email { get; set; } = string.Empty;

        public required string PasswordHash { get; set; }
    }

}