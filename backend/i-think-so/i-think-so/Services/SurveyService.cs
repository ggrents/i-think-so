using i_think_so.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace i_think_so.Services
{

    public interface ISurveyService
    {
        Task<List<Survey>> GetAllSurveys();
        Task<Survey> GetSurveyById(string id);
        Task<List<Survey>> GetSurveysByUserId(string userId);
        Task AddSurvey(Survey survey);
        Task<bool> UpdateSurvey(string id, Survey survey);
        Task<bool> DeleteSurvey(string id);
    }

    public class SurveyService : ISurveyService
    {
        private readonly IMongoCollection<Survey> _surveys;

        public SurveyService(IMongoDatabase database)
        {
            _surveys = database.GetCollection<Survey>("surveys");
        }

        public async Task<List<Survey>> GetAllSurveys()
        {
            return await _surveys.Find(_ => true).ToListAsync();
        }

        public async Task<Survey> GetSurveyById(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _surveys.Find(survey => survey.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<List<Survey>> GetSurveysByUserId(string userId)
        {
            return await _surveys.Find(survey => survey.UserId == userId).ToListAsync();
        }

        public async Task AddSurvey(Survey survey)
        {
            await _surveys.InsertOneAsync(survey);
        }

        public async Task<bool> UpdateSurvey(string id, Survey survey)
        {
            var objectId = ObjectId.Parse(id);
            var replaceOneResult = await _surveys.ReplaceOneAsync(s => s.Id == objectId, survey);
            return replaceOneResult.IsAcknowledged && replaceOneResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteSurvey(string id)
        {
            var objectId = ObjectId.Parse(id);
            var deleteResult = await _surveys.DeleteOneAsync(survey => survey.Id == objectId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
