using i_think_so.Domain.Entities;

namespace i_think_so.Application.Models.Request
{
    public class SurveyRequest
    {
        public required string Title { get; set; }
        public required string UserId { get; set; }
        public string? ImageUrl { get; set; }
        public List<Option>? Options { get; set; }
    }
}
