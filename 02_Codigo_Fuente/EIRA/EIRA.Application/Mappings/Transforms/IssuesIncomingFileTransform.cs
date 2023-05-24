using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Statics.Enumerations;
using EIRA.Application.Statics.Files;
using EIRA.Application.Statics.Jira;

namespace EIRA.Application.Mappings.Transforms
{
    public static class IssuesIncomingFileTransform
    {
        public static IssueCreateRequest ToIssueCreateRequest(this IssuesIncomingFile source, List<KeyValueList> responsibleList, KeyValueList defaultResponsible, RequestTypeTarget requestTypeTarget)
        {
            var output = new IssueCreateRequest
            {
                Project = new KeyableProp
                {
                    Key = source.Proyecto
                },
                Summary = source.Resumen.GetStringOrFallback("Sin Resumen"),
                Issuetype = new IdentifiableProp
                {
                    Id = GetIssueTypeId(requestTypeTarget, source.Proyecto.ToUpper())
                },
                Assignee = new IdentifiableProp
                {
                    Id = JiraConfiguration.Asignado
                },
                Compania = new ValuableProp
                {
                    Value = source.Compania == "SURTIGAS" ? "STG" : source.Compania
                },
                FechaAsignacionAranda = source.FechaAsignacion.ToUniversalTime(),
                NumeroAranda = source.NumeroCaso,

                DescripcionEstadoCliente = new Customfield1010
                {
                    Type = "doc",
                    Version = 1,
                    Content = new List<Customfield10103_Content>
                    {
                        new Customfield10103_Content
                        {
                            Type = "paragraph",
                            Content = new List<ContentContent>
                            {
                                new ContentContent
                                {
                                    Type = "text",
                                    Text = GetDescripcionEstadoCliente(source.EstadoCliente, source.Razon)
                                }
                            }
                        }
                    }
                },

                Gravedad = new IdentifiableProp
                {
                    Id = GetIdGravedad(source.Urgencia)
                }
            };

            if (source.FechaRegistro.HasValue)
            {
                output.FechaApertura = source.FechaRegistro.Value.ToUniversalTime();
            }

            if (!string.IsNullOrEmpty(source.Complejidad) && !string.IsNullOrEmpty(source.Complejidad.Trim()))
            {
                output.Complejidad = new ValuableProp
                {
                    Value = source.Complejidad.Trim(),
                };
            }

            if (!string.IsNullOrEmpty(source.ResponsableCliente) && !string.IsNullOrEmpty(source.ResponsableCliente.Trim()))
            {
                output.ResponsableCliente = new ValuableProp
                {
                    Value = GetResponsable(source.ResponsableCliente, responsibleList, defaultResponsible),
                };
            }

            if (source.Prioridad.HasValue)
            {
                output.Prioridad = source.Prioridad.Value;
            }


            if (!string.IsNullOrEmpty(source.Servicio) && !string.IsNullOrEmpty(source.Servicio.Trim()))
            {
                output.Frente = new ValuableProp
                {
                    Value = GetFrenteByServicio(source.Servicio) == "CONTABILIDAD" ? "BSS" : GetFrenteByServicio(source.Servicio)
                };
            }

            if (!string.IsNullOrEmpty(source.HistoriaUsuario) && !string.IsNullOrEmpty(source.HistoriaUsuario.Trim()))
            {
                output.HistoriaUsuario = new Customfield1010
                {
                    Type = "doc",
                    Version = 1,
                    Content = new List<Customfield10103_Content>
                    {
                        new Customfield10103_Content
                        {
                            Type="paragraph",
                            Content= new List<ContentContent>
                            {
                                new ContentContent
                                {
                                    Type="text",
                                    Text=source.HistoriaUsuario
                                }
                            }
                        }
                    }
                };
            }


            // FECHAS
            if (source.FechaEntregaAnalisisN1.HasValue && source.FechaEntregaAnalisisN1.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaEntregaAnalisisN1 = source.FechaEntregaAnalisisN1;
            }

            if (source.FechaEstimadaPropuestaSolucion.HasValue && source.FechaEstimadaPropuestaSolucion.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaEstimadaPropuestaSolucion = source.FechaEstimadaPropuestaSolucion;
            }

            if (source.FechaEntregaPropuestaSolucion.HasValue && source.FechaEntregaPropuestaSolucion.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaEntregaPropuestaSolucion = source.FechaEntregaPropuestaSolucion;
            }

            if (source.FechaEstimadaConstruccion.HasValue && source.FechaEstimadaConstruccion.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaEstimadaConstruccion = source.FechaEstimadaConstruccion;
            }

            if (source.FechaEntregaConstruccion.HasValue && source.FechaEntregaConstruccion.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaEntregaConstruccion = source.FechaEntregaConstruccion;
            }


            // TIEMPOS
            if (source.TiempoEstimadoPropuestaSolucion.HasValue)
            {
                output.TiempoEstimadoPropuestaSolucion = source.TiempoEstimadoPropuestaSolucion.Value;
            }

            if (source.TiempoEstimadoConstruccion.HasValue)
            {
                output.TiempoEstimadoConstruccion = source.TiempoEstimadoConstruccion.Value;
            }

            if (source.TiempoEstimadoSoportePruebas.HasValue)
            {
                output.TiempoEstimadoSoportePruebas = source.TiempoEstimadoSoportePruebas.Value;
            }


            // AIR-E
            if (!string.IsNullOrEmpty(source.ResponsablesMultiples) && !string.IsNullOrEmpty(source.ResponsablesMultiples.Trim()))
            {
                output.ResponsablesMultiples = source?.ResponsablesMultiples?.Split(";").Select(x => new ValuableProp { Value = x?.Trim() })?.ToList();
            }

            if (!string.IsNullOrEmpty(source.Reporte) && !string.IsNullOrEmpty(source.Reporte.Trim()))
            {
                output.Reporte = source?.Reporte?.Split(";").Select(x => new ValuableProp { Value = x?.Trim() })?.ToList();
            }

            if (source.FechaSolucion.HasValue && source.FechaSolucion.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaSolucion = source.FechaSolucion;
            }

            if (source.FechaCierre.HasValue && source.FechaCierre.Value.Year >= ExcelLimits.MinYear)
            {
                output.FechaCierre = source.FechaCierre;
            }


            return output;
        }

        private static CaseTypeIssueEnum GetCaseType(RequestTypeTarget requestTypeTarget)
        {
            return requestTypeTarget == RequestTypeTarget.Desarollo ? CaseTypeIssueEnum.Development : CaseTypeIssueEnum.Incident;
        }

        private static string GetIssueTypeId(RequestTypeTarget requestTypeTarget, string proyectKey)
        {
            if (proyectKey == "AS")
            {
                return "10015";
            }

            var caseType = GetCaseType(requestTypeTarget);

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

        private static string GetStringOrFallback(this string cadena, string fallback = "Sin contenido")
        {
            return string.IsNullOrEmpty(cadena) || string.IsNullOrEmpty(cadena.Trim()) ? fallback : cadena.Trim();
        }

        private static string GetFrenteByServicio(string servicio)
        {
            var servicioArr = servicio?.Split(" ");
            if (servicioArr.Length == 1)
                return servicio.Trim();

            return servicioArr[1];
        }

        private static string GetDescripcionEstadoCliente(string estado, string razon)
        {
            if (string.IsNullOrEmpty(estado) && string.IsNullOrEmpty(razon))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(estado))
                return razon;

            if (string.IsNullOrEmpty(razon))
                return estado;

            return $"{estado} - {razon}";
        }
    }
}
