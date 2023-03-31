using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Services.Files;
using EIRA.Infrastructure.FileManagers.Excel;
using EIRA.Infrastructure.Repositories.Persistence;
using EIRA.Infrastructure.Services.API.JIraAPIV3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EIRA.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            // Excel Service
            services.AddScoped<IExcelService, ExcelService>();

            // Services and repositories
            services.AddHttpClient < IIssuesService, IssuesService>();
            services.AddScoped<IIssuesService, IssuesService>();
            services.AddScoped<IIssuesJiraRepository, IssuesJiraRepository>();
        }
    }
}
