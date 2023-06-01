using EIRA.Application.Attributes;
using Newtonsoft.Json;

namespace EIRA.Application.Models.Files.Outgoing
{
    public class IssueConComentariosReport
    {
        [JsonProperty("project")]
        [ReportHeader("Proyecto")]
        public string Project { get; set; }
        [JsonProperty("customfield_10084")]
        [ReportHeader("Número de Caso")]
        public string NumeroCaso { get; set; }
        [JsonProperty("customfield_10065")]
        [ReportHeader("N1")]
        public string ResponsableCliente { get; set; }

        //[ReportHeader("Responsables")]
        //public string Responsables { get; set; }
        [JsonProperty("customfield_10087")]
        [ReportHeader("Complejidad")]
        public string Complejidad { get; set; }

        [JsonProperty("customfield_10089")]
        [ReportHeader("Prioridad")]
        public decimal? Prioridad { get; set; }

        [JsonProperty("status")]
        [ReportHeader("Estado")]
        public string Estado { get; set; }

        [JsonProperty("customfield_10103")]
        [ReportHeader("Descripción Corta")]
        public string DescripcionCorta { get; set; }// Descripcion

        [JsonProperty("customfield_10063")]
        [ReportHeader("Empresa")]
        public string Compania { get; set; }

        [JsonProperty("assignee")]
        [ReportHeader("Desarrollador")]
        public string Desarrollador { get; set; }
        //[ReportHeader("Fecha Estimada")]
        //public DateTime? FechaEstimada { get; set; } // Cuál fecha estimada es?

        [JsonProperty("comments")]
        [ReportHeader("Observaciones")]
        public string Observaciones { get; set; } // Comentarios

        [JsonProperty("customfield_10092")]
        [ReportHeader("Fecha de Entrega Análisis N1")]
        public DateTime? FechaEntregaAnalisisN1 { get; set; }

        [JsonProperty("customfield_10097")]
        [ReportHeader("Fecha de Entrega Propuesta Solución")]
        public DateTime? FechaEntregaPropuestaSolucion { get; set; }

        [JsonProperty("customfield_10094")]
        [ReportHeader("Fecha Entrega Construcción")]
        public DateTime? FechaEntregaConstruccion { get; set; }

        [JsonProperty("customfield_10101")]
        [ReportHeader("Fecha Cierre")]
        public DateTime? FechaCierre { get; set; }


        [JsonProperty("customfield_10096")]
        [ReportHeader("Fecha Estimada Propuesta Solución")] // Nuevo // También en serviceCalls
        public DateTime? FechaEstimadaPropuestaSolucion { get; set; }

        [JsonProperty("customfield_10095")]
        [ReportHeader("Fecha Estimada Construcción")] // Nuevo // También en serviceCalls
        public DateTime? FechaEstimadaConstruccion { get; set; }


        // TIEMPOS service calls
        [JsonProperty("customfield_10098")]
        [ReportHeader("Tiempo Estimado Propuesta Solución")]
        public decimal? TiempoEstimadoPropuestaSolucion { get; set; }

        [JsonProperty("customfield_10099")]
        [ReportHeader("Tiempo Estimado Construcción")]
        public decimal? TiempoEstimadoConstruccion { get; set; }

        [JsonProperty("customfield_10100")]
        [ReportHeader("Tiempo Estimado Soporte a Pruebas")]
        public decimal? TiempoEstimadoSoportePruebas { get; set; }


        [JsonProperty("customfield_10102")]
        [ReportHeader("Fecha de registro")]
        public DateTime? FechaRegistro { get; set; }

        [JsonProperty("customfield_10066")]
        [ReportHeader("Fecha Asignación")]
        public DateTime? FechaAsignacion { get; set; }

        [JsonProperty("customfield_10105")]
        [ReportHeader("Descripción Estado Cliente")] // ESTADO
        public string EstadoCliente { get; set; }


        /* NUEVOS */
        [JsonProperty("summary")]
        [ReportHeader("Resumen")]
        public string Summary { get; set; }

        [JsonProperty("issuetype")]
        [ReportHeader("Tipo de Incidente")]
        public string Issuetype { get; set; }

        [JsonProperty("customfield_10067")]
        [ReportHeader("Frente")]
        public string Frente { get; set; }
        
        // AIR-E
        [JsonProperty("customfield_10107")]
        [ReportHeader("Reporte")]
        public string Reporte { get; set; }

        [JsonProperty("customfield_10110")]
        [ReportHeader("Responsables AIR-E")]
        public string ResponsablesMultiples { get; set; }

        [JsonProperty("customfield_10111")]
        [ReportHeader("Responsable TRIPLEA_SUI")]
        public string ResponsablesMultiplesTripleaSUI { get; set; }

        [JsonProperty("customfield_10112")]
        [ReportHeader("Responsable TRIPLEA_CARTAS")]
        public string ResponsablesMultiplesTripleaCARTAS { get; set; }

        [JsonProperty("customfield_10109")]
        [ReportHeader("Fecha Solución")]
        public DateTime? FechaSolucion { get; set; }
    }
}
