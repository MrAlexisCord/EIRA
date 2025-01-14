﻿using EIRA.Application.Behaviours;
using EIRA.Application.Models.Configuration;
using EIRA.Application.Statics.Jira;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EIRA.Application
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Load Jira Configuration
            SetJiraConfiguration(configuration);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        public static void SetJiraConfiguration(IConfiguration configuration)
        {
            JiraConfiguration.Asignado = configuration["JiraConfiguration:Asignado"];
            JiraConfiguration.ProyectoId = configuration["JiraConfiguration:ProyectoId"];
            JiraConfiguration.Informador = configuration["JiraConfiguration:Informador"];
            JiraConfiguration.ResponsableCustomFieldId = configuration["JiraConfiguration:ResponsableCustomFieldId"];
            JiraConfiguration.IssueTypes = configuration.GetSection("JiraConfiguration:IssueTypes").Get<IssueTypeConfigModel>();
            JiraConfiguration.Gravedades = configuration.GetSection("JiraConfiguration:Gravedades").Get<List<GravedadConfigModel>>();
        }
    }
}
