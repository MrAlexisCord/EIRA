using EIRA.Application.Contracts.Auth;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Services;
using EIRA.Application.Services.API;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Services.Files;
using EIRA.Infrastructure.FileManagers.Excel;
using EIRA.Infrastructure.Repositories.Auth;
using EIRA.Infrastructure.Repositories.Auth.CacheRepository;
using EIRA.Infrastructure.Repositories.Persistence;
using EIRA.Infrastructure.Services;
using EIRA.Infrastructure.Services.API;
using EIRA.Infrastructure.Services.API.JIraAPIV3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EIRA.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // Cache Service
            services.AddMemoryCache();
            services.AddScoped<ICacheService, CacheService>();

            // Excel Service
            services.AddScoped<IExcelService, ExcelService>();

            // Token Service
            services.AddScoped<ITokenService, TokenService>();

            // Services and repositories
            services.AddHttpClient<IIssuesService, IssuesService>();
            services.AddHttpClient<IAuthService, AuthService>();

            services.AddScoped<IIssuesService, IssuesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthJiraRepository, AuthJiraRepository>();
            services.AddScoped<IAuthCacheRepository, AuthCacheRepository>();

            services.AddScoped<IIssuesJiraRepository, IssuesJiraRepository>();
        }
    }
}
