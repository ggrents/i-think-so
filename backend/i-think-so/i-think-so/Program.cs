using i_think_so.Application.Endpoints;
using i_think_so.Application.Repository;
using i_think_so.Domain.Entities;
using i_think_so.Infrastructure;
using i_think_so.Infrastructure.Database;
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/surveys", (ISurveyRepository repo, Survey survey) =>
{
    return repo.CreateSurveyAsync(survey);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapUserEndpoints();
app.MapSurveyEndpoints();

app.Run();

