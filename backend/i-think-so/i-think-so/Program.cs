using i_think_so.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace i_think_so
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton(new MongoClient(builder.Configuration.GetConnectionString("MongoDB")).GetDatabase(builder.Configuration.GetValue<string>("MongoDBSettings:DatabaseName")));
            builder.Services.AddScoped<ISurveyService, SurveyService>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}