using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Statics.Enumerations;
using EIRA.Application.Statics.Jira;

namespace EIRA.Application.Mappings.Transforms
{
    public static class IssuesIncomingFileTransform
    {
        public static IssueCreateRequest ToIssueCreateRequest(this IssuesIncomingFile source, List<KeyValueList> responsibleList, KeyValueList defaultResponsible)
        {
            var caseType = GetCaseType(source.TipoCaso);
            var output = new IssueCreateRequest
            {
                Assignee = new IdentifiableProp
                {
                    Id = JiraConfiguration.Asignado
                },
                Description = new Description
                {
                    Content = new List<DescriptionContent>
                    {
                        new DescriptionContent
                        {
                            Type = "paragraph",
                            Content = new List<ContentContent> {
                                new ContentContent
                                {
                                    Text = source.Summary,
                                    Type = "text",
                                }
                            }
                        }
                    },
                    Type = "doc",
                    Version = 1
                },
                Project = new IdentifiableProp
                {
                    Id = JiraConfiguration.ProyectoId
                },
                Summary = source.Summary,
                Issuetype = new IdentifiableProp
                {
                    Id = GetIssueTypeId(source.TipoCaso)
                },
                Priority = new NameableProp
                {
                    Name = "Medium"
                },
                Frente = new ValuableProp
                {
                    Value = source.Servicio
                },
                Compania = new ValuableProp
                {
                    Value = "CEO"
                },
                NumeroAranda = source.NumeroCaso,
                FechaAsignacionAranda = DateTime.UtcNow,
                ResponsableCliente = new ValuableProp
                {
                    Value = GetResponsable(source.GestionadoPor, responsibleList, defaultResponsible),
                },

                FechaRegistroAranda = source.FechaRegistro.ToUniversalTime(),
                EstadoAranda = $"{source.Estado} - {source.Razon}",
                Grupo = source.Grupo?.Split("-")?.LastOrDefault()?.Trim() ?? string.Empty,
                SistemaCargue = "EIRA",
            };

            switch (caseType)
            {
                case CaseTypeIssueEnum.Incident:
                case CaseTypeIssueEnum.Development:
                    output.Gravedad = new IdentifiableProp
                    {
                        Id = GetIdGravedad(source.Urgencia),
                    };
                    break;
            }

            return output;
        }

        private static CaseTypeIssueEnum GetCaseType(string tipoCaso)
        {
            if (string.IsNullOrEmpty(tipoCaso) || tipoCaso.Trim() == string.Empty)
                return CaseTypeIssueEnum.None;

            return tipoCaso.ToUpper().Contains("REQUERIMIENTO") ? CaseTypeIssueEnum.Development : CaseTypeIssueEnum.Incident;
        }

        private static string GetIssueTypeId(string tipoCaso)
        {
            var caseType = GetCaseType(tipoCaso);

            return caseType switch
            {
                CaseTypeIssueEnum.Incident => JiraConfiguration.IssueTypes.Incidente,
                CaseTypeIssueEnum.Development => JiraConfiguration.IssueTypes.Desarrollo,
                _ => null,
            };
        }

        private static string GetIdGravedad(string urgencia)
        {
            if (string.IsNullOrEmpty(urgencia))
                return null;

            var gravedad = JiraConfiguration.Gravedades.FirstOrDefault(x => x.Urgencias.Contains(urgencia.ToUpper()));
            return gravedad.GravedadId;
        }


        private static string GetResponsable(string gestionadoPor, List<KeyValueList> responsibleList, KeyValueList defaultResponsible)
        {
            if (string.IsNullOrEmpty(gestionadoPor) || string.IsNullOrEmpty(gestionadoPor.Trim()))
                return defaultResponsible.Value;

            var responsibleExists = responsibleList.Any(x => x.Value.ToUpper() == (gestionadoPor).ToUpper());

            if (responsibleExists)
                return responsibleList.FirstOrDefault(x => x.Value.ToUpper() == (gestionadoPor).ToUpper()).Value;

            return defaultResponsible.Value;
        }
    }
}
