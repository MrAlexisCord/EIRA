using EIRA.Application.Models.Configuration;

namespace EIRA.Application.Statics.Jira
{
    public static class JiraConfiguration
    {
        public static string Asignado { get; set; }
        public static string ProyectoId { get; set; }
        public static string Informador { get; set; }
        public static string ResponsableCustomFieldId { get; set; }
        public static IssueTypeConfigModel IssueTypes { get; set; }
        public static List<GravedadConfigModel> Gravedades { get; set; }
    }
}
