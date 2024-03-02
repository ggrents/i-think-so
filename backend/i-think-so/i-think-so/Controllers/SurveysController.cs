using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System;
using i_think_so.Models;
using i_think_so.Services;

namespace i_think_so.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        // GET: api/surveys
        [HttpGet]
        public async Task<ActionResult<List<Survey>>> Get()
        {
            var surveys = await _surveyService.GetAllSurveys();
            return Ok(surveys);
        }

        // GET: api/surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetById(string id)
        {
            var survey = await _surveyService.GetSurveyById(id);

            if (survey == null)
            {
                return NotFound();
            }

            return Ok(survey);
        }

        // POST: api/surveys
        [HttpPost]
        public async Task<ActionResult<Survey>> Post(Survey survey)
        {
            await _surveyService.AddSurvey(survey);
            return CreatedAtAction(nameof(GetById), new { id = survey.Id }, survey);
        }

        // PUT: api/surveys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Survey survey)
        {
            var updated = await _surveyService.UpdateSurvey(id, survey);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/surveys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _surveyService.DeleteSurvey(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}