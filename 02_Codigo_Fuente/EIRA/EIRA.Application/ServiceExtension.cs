using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace EIRA.Application
{
    public static class ServiceExtension
    {

        // PEND: IConfiguration configuration  => using Microsoft.Extensions.Configuration;
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

        }
    }
}
