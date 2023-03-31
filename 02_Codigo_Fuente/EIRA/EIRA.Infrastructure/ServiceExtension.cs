using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.IssueType;
using EIRA.Application.Services.Files;
using EIRA.Infrastructure.FileManagers.Excel;
using EIRA.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EIRA.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IJiraRepository<>), typeof(JiraRepository<>));
            services.AddScoped<IIssueTypeRepository, IssueTypeRepository>();
            services.AddScoped<IExcelService, ExcelService>();
        }
    }
}
