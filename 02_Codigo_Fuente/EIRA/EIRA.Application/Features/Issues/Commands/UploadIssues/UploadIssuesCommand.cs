using EIRA.Application.Models.Files.Incoming;
using MediatR;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommand: IRequest<List<IssuesIncomingFile>>
    {
        public Stream FileStream { get; set; }
    }
}
