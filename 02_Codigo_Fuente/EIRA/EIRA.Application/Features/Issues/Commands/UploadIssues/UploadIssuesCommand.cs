using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;
using MediatR;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommand: IRequest<List<JiraUploadIssueErrorLog>>
    {
        public Stream FileStream { get; set; }
    }
}
