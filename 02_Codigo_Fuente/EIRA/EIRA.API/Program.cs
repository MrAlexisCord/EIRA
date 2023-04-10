using EIRA.Application;
using EIRA.Application.Statics;
using EIRA.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add HttpClientServices

// Set Static External APIs URLs
ExternalEndpoint.JiraAPIBaseV3 = builder.Configuration["ExternalAPIUrls:JiraAPIV3"];


// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        builder.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
