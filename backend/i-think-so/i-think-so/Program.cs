using i_think_so.Application.Endpoints;
using i_think_so.Application.Extensions;
using i_think_so.Application.Models.Request;
using i_think_so.Application.Repository;
using i_think_so.Application.Services;
using i_think_so.Application.Services.Auth;
using i_think_so.Application.Services.Token;
using i_think_so.Infrastructure;
using i_think_so.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.Configure<SecurityOptions>(
           builder.Configuration.GetSection(SecurityOptions.Security));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var securitySection = builder.Configuration.GetSection("Security");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = securitySection["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = securitySection["Audience"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitySection["Secret"]!)),
                    };
                });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "I-Think-So_API",
        Version = "0.0.1",
        Contact = new OpenApiContact
        {
            Name = "Grents Artem",
            Url = new Uri("https://github.com/ggrents")
        }
    });

    var securityScheme = new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new [] { "Bearer"} }
                });
});


builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiExceptionHandling();
app.UseHttpsRedirection();

app.MapUserEndpoints();
app.MapSurveyEndpoints();


app.Run();

