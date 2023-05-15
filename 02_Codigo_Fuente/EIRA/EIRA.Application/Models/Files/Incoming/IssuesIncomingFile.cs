using EIRA.Application.Attributes;

namespace EIRA.Application.Models.Files.Incoming
{
    public class IssuesIncomingFile
    {
        //[ReportHeader("TIPO_CASO")]
        //public string TipoCaso { get; set; } = "";



        [ReportHeader("No. Caso")] // NO_CASO
        public string NumeroCaso { get; set; }

        [ReportHeader("Servicio")] // SERVICIO
        public string Servicio { get; set; }

        [ReportHeader("Compañía")]
        public string Compania { get; set; }

        [ReportHeader("Urgencia")]
        public string Urgencia { get; set; }

        [ReportHeader("Fecha de registro")]
        public DateTime? FechaRegistro { get; set; }

        [ReportHeader("Fecha Asignación")]
        public DateTime FechaAsignacion { get; set; }

        [ReportHeader("Responsable Cliente")] //GESTIONADO_POR
        public string ResponsableCliente { get; set; }

        [ReportHeader("Historia de Usuario")] // Nuevo
        public string HistoriaUsuario { get; set; }

        [ReportHeader("Responsable")] // Por defecto Anay
        public string Responsable { get; set; }

        [ReportHeader("Prioridad")] // Nuevo // Sólo en incidents
        public int? Prioridad { get; set; }

        [ReportHeader("Complejidad")] // Nuevo
        public string Complejidad { get; set; }

        [ReportHeader("Estado Cliente")] // ESTADO
        public string EstadoCliente { get; set; }

        [ReportHeader("Razón")] // RAZON
        public string Razon { get; set; }

        // FECHAS INCIDENTS
        [ReportHeader("Fecha Entrega Análisis N1")] // Nuevo
        public DateTime? FechaEntregaAnalisisN1 { get; set; }
        [ReportHeader("Fecha Estimada Propuesta Solución")] // Nuevo // También en serviceCalls
        public DateTime? FechaEstimadaPropuestaSolucion { get; set; }
        [ReportHeader("Fecha Entrega Propuesta Soluciónn")] // Nuevo // También en serviceCalls
        public DateTime? FechaEntregaPropuestaSolucion { get; set; }
        [ReportHeader("Fecha Estimada Construcción")] // Nuevo // También en serviceCalls
        public DateTime? FechaEstimadaConstruccion { get; set; }
        [ReportHeader("Fecha Entrega Construcción")] // Nuevo  // También en serviceCalls
        public DateTime? FechaEntregaConstruccion { get; set; }


        // TIEMPOS service calls
        [ReportHeader("Tiempo Estimado Propuesta Solución")]
        public decimal? TiempoEstimadoPropuestaSolucion { get; set; }

        [ReportHeader("Tiempo Estimado Construcción")]
        public decimal? TiempoEstimadoConstruccion { get; set; }

        [ReportHeader("Tiempo Estimado Soporte a Pruebas")]
        public decimal? TiempoEstimadoSoportePruebas { get; set; }

        // Opcionales
        [ReportHeader("Resumen")] // NO_CASO
        public string Resumen { get; set; }

        [ReportHeader("Comentarios")] // NO_CASO
        public string Comentarios { get; set; }











        //[ReportHeader("Cliente")]
        //public string Cliente { get; set; }

        //[ReportHeader("GRUPO")]
        //public string Grupo { get; set; }


        //[ReportHeader("Asunto")]//ASUNTO
        //public string Summary { get; set; }


        //[ReportHeader("TIPO_CASO")]
        //public string TipoCaso { get; set; }
    }
}
