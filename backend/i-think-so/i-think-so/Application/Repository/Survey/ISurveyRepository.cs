using i_think_so.Domain.Entities;

namespace i_think_so.Application.Repository
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> GetAllSurveysAsync();
        Task<Survey> GetSurveyByIdAsync(string id);
        Task CreateSurveyAsync(Survey survey);
        Task<bool> UpdateSurveyAsync(string id, Survey survey);
        Task<bool> DeleteSurveyAsync(string id);
    }
}
