using EIRA.Application.Attributes;

namespace EIRA.Application.Models.Files.Outgoing
{
    public class IssueConComentariosReport
    {
        [ReportHeader("Issue Id")]
        public string IssueKeyOrId { get; set; }
        [ReportHeader("Número Aranda")]
        public string NumeroAranda { get; set; }
        [ReportHeader("Entero")]
        public int Entero { get; set; }
        [ReportHeader("Entero Nullable")]
        public int? EnteroNullable { get; set; }
        [ReportHeader("Fecha")]
        public DateTime Fecha { get; set; }
        [ReportHeader("Fecha Nullable")]
        public DateTime? FechaNullable { get; set; }
    }
}
