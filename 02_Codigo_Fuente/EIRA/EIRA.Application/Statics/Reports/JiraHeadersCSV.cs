using EIRA.Application.Models.LogModels;

namespace EIRA.Application.Statics.Reports
{
    public static class JiraHeadersCSV
    {
        public static string[] ISSUES_ERROR_LOG_HEADERS = new string[]
        {
            nameof(JiraUploadIssueErrorLog.Proyecto),
            nameof(JiraUploadIssueErrorLog.NumeroAranda),
            nameof(JiraUploadIssueErrorLog.IssueKeyOrId),
            nameof(JiraUploadIssueErrorLog.ErrorMessage),
            nameof(JiraUploadIssueErrorLog.Operation),
        };
    }
}
