using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Statics.Enumerations;

namespace EIRA.Application.Mappings.Transforms
{
    public static class IssuesIncomingFileTransform
    {
        public static IssueCreateRequest ToIssueCreateRequest(this IssuesIncomingFile source)
        {
            // Anay Id Account: 6229183c302c6b006af604be

            var caseType = GetCaseType(source.TipoCaso);
            var output = new IssueCreateRequest
            {
                Assignee = new IdentifiableProp
                {
                    Id = "557058:8032e131-f9c8-4080-a44e-1e707323b32a"
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
                    Id = "10000"
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
                NumeroAranda = source.NumeroCaso.Value,
                FechaAsignacionAranda = DateTime.UtcNow,
                ResponsableCliente = new ValuableProp
                {
                    Value = source.GestionadoPor
                },
            };

            switch (caseType)
            {
                case CaseTypeIssueEnum.Incident:
                    output.Gravedad = new IdentifiableProp
                    {
                        Id = GetIdGravedad(source.Urgencia),
                    };
                    break;

                case CaseTypeIssueEnum.Development:
                    output.GravedadDesarrollo = new IdentifiableProp
                    {
                        Id = GetIdGravedadDesarrollo(source.Urgencia),
                    };
                    break;
            }

            return output;
        }

        private static CaseTypeIssueEnum GetCaseType(string tipoCaso)
        {
            if (string.IsNullOrEmpty(tipoCaso) || tipoCaso.Trim() == string.Empty)
                return CaseTypeIssueEnum.None;

            return tipoCaso.ToUpper().Contains("INCIDEN") ? CaseTypeIssueEnum.Incident : CaseTypeIssueEnum.Development;
        }

        private static string GetIssueTypeId(string tipoCaso)
        {
            var caseType = GetCaseType(tipoCaso);

            return caseType switch
            {
                CaseTypeIssueEnum.Incident => "10009",
                CaseTypeIssueEnum.Development => "10010",
                _ => null,
            };
        }

        private static string GetIdGravedad(string urgencia)
        {
            return urgencia.ToUpper() switch
            {
                "BAJA" or "BAJO" => "10120",
                "MEDIA" or "MEDIO" => "10119",
                "ALTA" or "ALTO" => "10118",
                "CRITICA" or "CRITICO" => "10117",
                _ => null,
            };
        }

        private static string GetIdGravedadDesarrollo(string urgencia)
        {
            return urgencia.ToUpper() switch
            {
                "NO APLICA" or "NO APLICO" => "10140",
                "BAJA" or "BAJO" => "10144",
                "MEDIA" or "MEDIO" => "10143",
                "ALTA" or "ALTO" => "10142",
                "CRITICA" or "CRITICO" => "10141",
                _ => null,
            };
        }
    }
}
