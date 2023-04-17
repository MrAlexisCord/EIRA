using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Exceptions;
using EIRA.Application.Extensions;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.Files;
using MediatR;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommandHandler : IRequestHandler<UploadIssuesCommand, List<JiraUploadIssueErrorLog>>
    {
        private readonly IExcelService _excelService;
        private readonly IIssuesJiraRepository _issuesJiraRepository;

        public UploadIssuesCommandHandler(IExcelService excelService,
            IIssuesJiraRepository issuesJiraRepository)
        {
            _excelService = excelService;
            _issuesJiraRepository = issuesJiraRepository;
        }

        public async Task<List<JiraUploadIssueErrorLog>> Handle(UploadIssuesCommand request, CancellationToken cancellationToken)
        {
            var logRespose = new List<JiraUploadIssueErrorLog>();

            var headers = PropertyExtension.GetReportHeadersDictionary<IssuesIncomingFile>();
            var response = _excelService.ReadExcel<IssuesIncomingFile>(request.FileStream, headers);

            var validIssuesList = response?.Where(x =>
            (!string.IsNullOrEmpty(x.NumeroCaso) && !string.IsNullOrEmpty(x.NumeroCaso.Trim()))
            && x.Grupo.ToUpper().Contains("OLSOFT"));

            if (validIssuesList is not null && validIssuesList.Any())
            {
                logRespose = await _issuesJiraRepository.PostIssues(validIssuesList.ToList());
            }
            else
            {
                throw new NullFileException("El archivo no contiene issues válidos para la operación");
            }

            return logRespose;
        }
    }
}
