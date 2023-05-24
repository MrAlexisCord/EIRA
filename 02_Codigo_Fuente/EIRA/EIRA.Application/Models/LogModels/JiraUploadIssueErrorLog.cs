using EIRA.Application.Attributes;

namespace EIRA.Application.Models.LogModels
{
    public class JiraUploadIssueErrorLog
    {
        [ReportHeader("Operación")]
        public string Operation { get; set; }
        [ReportHeader("Issue")]
        public string IssueKeyOrId { get; set; }
        [ReportHeader("Incidente")]
        public string NumeroAranda { get; set; }
        [ReportHeader("Error")]
        public string ErrorMessage { get; set; }
    }
}
