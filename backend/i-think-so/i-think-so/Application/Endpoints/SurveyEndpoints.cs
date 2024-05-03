using i_think_so.Application.Models.Request;
using i_think_so.Application.Repository;
using i_think_so.Application.Services.Auth;
using i_think_so.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace i_think_so.Application.Endpoints
{
    public static class SurveyEndpoints
    {
        public static void MapSurveyEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/api/surveys").WithTags("Surveys");

            group.MapGet("", GetSurveys).WithDescription("Get list of surveys");
            group.MapGet("{id}", GetSurveyById);
            group.MapGet("user/{id}", GetSurveyByUser);
            group.MapGet("voted", GetUserVotedSurveys);
            group.MapPost("", CreateSurvey);
            group.MapDelete("{id}", DeleteSurvey);
            group.MapPut("{id}", UpdateSurvey);
        }

        public async static Task<IResult> GetSurveys(ISurveyRepository repo, CancellationToken cancellationToken)
        {
            return Results.Ok(await repo.GetAllSurveysAsync(cancellationToken));
        }

        public async static Task<IResult> GetSurveyById(string id, ISurveyRepository repo, CancellationToken cancellationToken)
        {
            return Results.Ok(await repo.GetSurveyByIdAsync(id, cancellationToken));
        }

        public async static Task<IResult> CreateSurvey(SurveyRequest request, HttpContext httpContext, 
            ISurveyRepository repo, CancellationToken cancellationToken)
        {
            try
            {
                await repo.CreateSurveyAsync(httpContext, request, cancellationToken);
                return Results.Ok("Survey was created successfully");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> DeleteSurvey(ISurveyRepository repo, string id, CancellationToken cancellationToken)
        {
            var deleted = await repo.DeleteSurveyAsync(id, cancellationToken);
            if (deleted)
            {
                return Results.NoContent();
            }
            else
            {
                return Results.NotFound("Survey not found or could not be deleted.");
            }
        }

        public async static Task<IResult> UpdateSurvey(string id, SurveyRequest request,
            HttpContext httpContext, ISurveyRepository repo, CancellationToken cancellationToken)
        {
            try
            {
                await repo.UpdateSurveyAsync(id, request, cancellationToken);
                return Results.Ok("Survey was updated successfully");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public async static Task<IResult> GetSurveyByUser([FromRoute] string id, ISurveyRepository repo, CancellationToken cancellationToken)
        {
            return Results.Ok(await repo.GetSurveysByUserIdAsync(id, cancellationToken));
        }

        public async static Task<IResult> GetUserVotedSurveys(string userId, ISurveyRepository repo, CancellationToken cancellationToken)
        {
            return Results.Ok(await repo.GetUserVotedSurveysAsync(userId, cancellationToken));
        }
    }
}
