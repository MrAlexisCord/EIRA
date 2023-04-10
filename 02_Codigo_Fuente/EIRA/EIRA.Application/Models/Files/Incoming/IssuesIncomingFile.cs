using EIRA.Application.Attributes;

namespace EIRA.Application.Models.Files.Incoming
{
    public class IssuesIncomingFile
    {
        [ReportHeader("TIPO_CASO")]
        public string TipoCaso { get; set; }

        [ReportHeader("NO_CASO")]
        public int? NumeroCaso { get; set; }

        [ReportHeader("ASUNTO")]
        public string Summary { get; set; }

        [ReportHeader("GESTIONADO_POR")]
        public string GestionadoPor { get; set; }

        [ReportHeader("FECHA_DE_REGISTRO")]
        public string FechaRegistro { get; set; }

        [ReportHeader("ESTADO")]
        public string Estado { get; set; }

        [ReportHeader("SERVICIO")]
        public string Servicio { get; set; }

        [ReportHeader("RESPONSABLE")]
        public string Responsable { get; set; }

        [ReportHeader("RAZON")]
        public string Razon { get; set; }

        [ReportHeader("URGENCIA")]
        public string Urgencia { get; set; }

        [ReportHeader("GRUPO")]
        public string Grupo { get; set; }

        [ReportHeader("COMPANIA")]
        public string Compania { get; set; }
    }
}
