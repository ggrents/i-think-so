using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

#pragma warning disable
namespace i_think_so.Domain.Entities
{
    public class Survey
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Username { get; set; }
        public string ImageUrl { get; set; }
        public List<Option> Options { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
#pragma warning restore