using MongoDB.Bson;

namespace i_think_so.Models
{
    public class Survey
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public ObjectId UserId { get; set; }
        public List<Option>? Options { get; set; }
    }
}
