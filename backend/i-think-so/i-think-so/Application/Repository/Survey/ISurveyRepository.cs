using i_think_so.Application.Models.Request;
using i_think_so.Domain.Entities;

namespace i_think_so.Application.Repository
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> GetAllSurveysAsync(CancellationToken cancellationToken);
        Task<Survey> GetSurveyByIdAsync(string id, CancellationToken cancellationToken);
        Task CreateSurveyAsync(HttpContext httpContext, SurveyRequest survey, CancellationToken cancellationToken);
        Task<bool> UpdateSurveyAsync(string id, SurveyRequest survey, CancellationToken cancellationToken);
        Task<bool> DeleteSurveyAsync(string id, CancellationToken cancellationToken);
        Task<List<Survey>> GetSurveysByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<List<Survey>> GetUserVotedSurveysAsync(string userId, CancellationToken cancellationToken);
    }
}
