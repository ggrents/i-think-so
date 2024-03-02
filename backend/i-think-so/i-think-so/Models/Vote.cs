using MongoDB.Bson;

namespace i_think_so.Models
{
    public class Vote
    {
        public ObjectId Id { get; set; } 
        public ObjectId UserId { get; set; }
        public DateTime VotedAt { get; set; }
    }
    
}
