using EIRA.Application.Attributes;

namespace EIRA.Application.Models.Files.Outgoing
{
    public class IssueConComentariosReport
    {
        //[ReportHeader("Proyecto")]
        //public string Project { get; set; }
        [ReportHeader("Número de Caso")]
        public string NumeroCaso { get; set; }
        [ReportHeader("N1")]
        public string ResponsableCliente { get; set; } //N1
        //[ReportHeader("Tarea")]
        //public string Tarea { get; set; } = "Obtener Tarea";
        [ReportHeader("Complejidad")]
        public string Complejidad { get; set; }
        [ReportHeader("Prioridad")]
        public decimal? Prioridad { get; set; }
        [ReportHeader("Estado")]
        public string Estado { get; set; }
        [ReportHeader("Descripción Corta")]
        public string DescripcionCorta { get; set; }// Descripcion
        [ReportHeader("Empresa")]
        public string Compania { get; set; }
        [ReportHeader("Desarrollador")]
        public string Desarrollador { get; set; }
        //[ReportHeader("Fecha Estimada")]
        //public DateTime? FechaEstimada { get; set; } // Cuál fecha estimada es?
        [ReportHeader("Observaciones")]
        public string Observaciones { get; set; } // Comentarios
        [ReportHeader("Fecha de Entrega Análisis N1")]
        public DateTime? FechaEntregaAnalisisN1 { get; set; }
        [ReportHeader("Fecha de Entrega Propuesta Solución")]
        public DateTime? FechaEntregaPropuestaSolucion { get; set; }
        [ReportHeader("Fecha Entrega Construcción")]
        public DateTime? FechaEntregaConstruccion { get; set; }
        [ReportHeader("Fecha Cierre")]
        public DateTime? FechaCierre { get; set; }
    }
}
