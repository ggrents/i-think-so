namespace i_think_so.Application.Endpoints
{
    public static class SurveyEndpoints
    {
        public static void MapSurveyEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/api/surveys").WithTags("Surveys");

            group.MapGet("", GetSurveys).WithDescription("Get list of surveys");
            group.MapGet("{id}", GetSurveyById);
            group.MapGet("user/{id}", GetSurveyById);
            group.MapGet("voted", GetSurveyById);
            group.MapPost("{id}", CreateSurvey);
            group.MapDelete("{id}", DeleteSurvey);
            group.MapPut("{id}", UpdateSurvey);
        }

        public static IResult GetSurveys(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

        public static IResult GetSurveyById(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

        public static IResult CreateSurvey(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

        public static IResult DeleteSurvey(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

        public static IResult UpdateSurvey(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }
    }
}
