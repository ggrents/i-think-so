
using i_think_so.Domain.Entities;
using i_think_so.Infrastructure.Database;
using MongoDB.Driver;

namespace i_think_so.Application.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IMongoCollection<Survey> _surveysCollection;

        public SurveyRepository(MongoContext mongoContext)
        {
            _surveysCollection = mongoContext.Surveys;
        }

        public async Task<List<Survey>> GetAllSurveysAsync()
        {
            return await _surveysCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Survey> GetSurveyByIdAsync(string id)
        {
            return await _surveysCollection.Find(survey => survey.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateSurveyAsync(Survey survey)
        {
            await _surveysCollection.InsertOneAsync(survey);
        }

        public async Task<bool> UpdateSurveyAsync(string id, Survey survey)
        {
            var result = await _surveysCollection.ReplaceOneAsync(s => s.Id == id, survey);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteSurveyAsync(string id)
        {
            var result = await _surveysCollection.DeleteOneAsync(survey => survey.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
