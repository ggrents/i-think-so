
using i_think_so.Application.Models.Request;
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

        public async Task<List<Survey>> GetAllSurveysAsync(CancellationToken cancellationToken)
        {
            return await _surveysCollection.Find(_ => true).ToListAsync(cancellationToken);
        }

        public async Task<Survey> GetSurveyByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _surveysCollection.Find(survey => survey.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

#pragma warning disable
        public async Task CreateSurveyAsync(HttpContext httpContext, SurveyRequest surveyRequest, CancellationToken cancellationToken)
        {
            
            var survey = new Survey
            {
                Title = surveyRequest.Title,
                Username = httpContext.User.Identity.Name,
                ImageUrl = surveyRequest.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                Options = surveyRequest.Options?.Select(option => new Option
                {
                    Title = option.Title,
                    Votes = null
                }).ToList()
            };

            await _surveysCollection.InsertOneAsync(survey, null, cancellationToken);
        }


        public async Task<bool> UpdateSurveyAsync(string id, SurveyRequest survey, CancellationToken cancellationToken)
        {
            var existingSurvey = await _surveysCollection.FindOneAndUpdateAsync(
                Builders<Survey>.Filter.Eq(s => s.Id, id),
                Builders<Survey>.Update
                    .Set(s => s.Title, survey.Title)
                    .Set(s => s.ImageUrl, survey.ImageUrl)
                    .Set(s => s.Options, survey.Options),
                new FindOneAndUpdateOptions<Survey> { ReturnDocument = ReturnDocument.After },
                cancellationToken);

            return existingSurvey != null;
        }
#pragma warning restore
        public async Task<bool> DeleteSurveyAsync(string id, CancellationToken cancellationToken)
        {
            var result = await _surveysCollection.DeleteOneAsync(survey => survey.Id == id, cancellationToken);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<List<Survey>> GetSurveysByUserIdAsync(string username, CancellationToken cancellationToken)
        {
            return await _surveysCollection.Find(survey => survey.Username == username).ToListAsync(cancellationToken);
        }

        public async Task<List<Survey>> GetUserVotedSurveysAsync(string username, CancellationToken cancellationToken)
        {
            return await _surveysCollection.Find(survey => survey.Options.Any(option => option.Votes.Any(vote => vote.Username == username)))
                                                                         .ToListAsync(cancellationToken);
        }
    }
}
