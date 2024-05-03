namespace i_think_so.Application.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/account").WithTags("Account");

            group.MapGet("register", Register);
            group.MapGet("login", Login);
            group.MapGet("self", Self);
        }

        public static IResult Register(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

        public static IResult Login(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

        public static IResult Self(HttpContext context, CancellationToken cancellationToken)
        {
            return Results.Empty;
        }

    }
}
