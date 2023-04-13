using EIRA.Application;
using EIRA.Application.Statics;
using EIRA.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Set Static External APIs URLs
ExternalEndpoint.JiraAPIBaseV3 = builder.Configuration["ExternalAPIUrls:JiraAPIV3"];

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["EIRAApiKey"])),
        ClockSkew = TimeSpan.Zero,

    }).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer",
                }
            }, new string[]{ }
        }
    });
});

var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        builder.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.WithExposedHeaders("content-disposition", "attachments");
        builder.WithExposedHeaders("content-disposition");
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        builder.SetIsOriginAllowed(origin => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(devCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
