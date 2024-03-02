using MongoDB.Bson;

namespace i_think_so.Models
{
    public class Option
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public List<Vote>? Votes { get; set; }
        public ObjectId UserId { get; set; }

    }
}
