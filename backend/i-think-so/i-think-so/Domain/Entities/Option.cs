namespace i_think_so.Domain.Entities
{
    public class Option
    {
        public required string Title { get; set; }
        public List<Vote>? Votes { get; set; }
    }
}
