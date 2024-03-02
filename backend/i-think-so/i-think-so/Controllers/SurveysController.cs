using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System;

namespace i_think_so.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveysController : ControllerBase
    {
        IMongoDatabase _db;
        public SurveysController(IMongoDatabase db)
        {
            _db = db;
        }
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            var surveys = await _db.GetCollection<BsonDocument>("surveys").Find(_ => true).ToListAsync();
            return surveys.ToJson();
        }
    }
}