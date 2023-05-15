using EIRA.Application.Models.LogModels;
using MediatR;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommand: IRequest<List<JiraUploadIssueErrorLog>>
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
    }
}
