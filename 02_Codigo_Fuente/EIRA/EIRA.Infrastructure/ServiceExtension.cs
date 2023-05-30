using EIRA.Application.Contracts.Auth;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.Services;
using EIRA.Application.Services.API;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Services.Files;
using EIRA.Infrastructure.DbContexts.SqlServerContexts;
using EIRA.Infrastructure.FileManagers.CSV;
using EIRA.Infrastructure.FileManagers.Excel;
using EIRA.Infrastructure.Repositories.Auth;
using EIRA.Infrastructure.Repositories.Auth.CacheRepository;
using EIRA.Infrastructure.Repositories.Eira;
using EIRA.Infrastructure.Repositories.Persistence;
using EIRA.Infrastructure.Repositories.Persistence.CacheRepository;
using EIRA.Infrastructure.Services;
using EIRA.Infrastructure.Services.API;
using EIRA.Infrastructure.Services.API.JIraAPIV3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EIRA.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server
            services.AddDbContext<EiraContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("defaultConnection"))
            );

            //Generic Repository
            services.AddScoped(typeof(IAsyncRepository<>), typeof(ApplicationRepository<>));

            // Cache Service
            services.AddMemoryCache();
            services.AddScoped<ICacheService, CacheService>();

            // File Services
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<ICSVService, CSVService>();

            // Token Service
            services.AddScoped<ITokenService, TokenService>();

            // HttpClient for services
            services.AddHttpClient<IIssuesService, IssuesService>();
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddHttpClient<IResponsibleService, ResponsibleService>();
            services.AddHttpClient<IStatusesService, StatusesService>();
            services.AddHttpClient<IProjectsService, ProjectsService>();

            // Services and repositories
            services.AddScoped<IIssuesService, IssuesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IResponsibleService, ResponsibleService>();
            services.AddScoped<IStatusesService, StatusesService>();
            services.AddScoped<IProjectsService, ProjectsService>();

            services.AddScoped<IAuthJiraRepository, AuthJiraRepository>();
            services.AddScoped<IAuthCacheRepository, AuthCacheRepository>();
            services.AddScoped<IResponsibleCacheRepository, ResponsibleCacheRepository>();

            services.AddScoped<IIssuesJiraRepository, IssuesJiraRepository>();
            services.AddScoped<IResponsibleJiraRepository, ResponsibleJiraRepository>();
            services.AddScoped<IStatusesRepository, StatusesRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
        }
    }
}
