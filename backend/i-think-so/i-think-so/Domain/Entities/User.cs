using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace i_think_so.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        public required string Username { get; set; }

        public string? Email { get; set; } = string.Empty;

        public required byte[] PasswordHash { get; set; }
    }

}