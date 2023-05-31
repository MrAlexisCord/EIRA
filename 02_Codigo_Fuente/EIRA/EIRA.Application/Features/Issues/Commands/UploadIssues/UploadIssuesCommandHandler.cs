using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Exceptions;
using EIRA.Application.Extensions;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.Files;
using EIRA.Application.Statics.Enumerations;
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
            List<JiraUploadIssueErrorLog> logRespose;

            var requestTypeTarget = GetRequestTypeByFileName(request.FileName);
            var headers = PropertyExtension.GetReportHeadersDictionary<IssuesIncomingFile>();
            var response = _excelService.ReadExcel<IssuesIncomingFile>(request.FileStream, headers);

            var validIssuesList = response
                ?.Where(x => !string.IsNullOrEmpty(x.NumeroCaso) && !string.IsNullOrEmpty(x.NumeroCaso.Trim()));

            if (validIssuesList is not null && validIssuesList.Any())
            {
                logRespose = await _issuesJiraRepository.PostIssuesAsync(validIssuesList.ToList(), requestTypeTarget);
            }
            else
            {
                throw new NullFileException("El archivo no contiene issues válidos para la operación");
            }

            return logRespose;
        }

        private RequestTypeTarget GetRequestTypeByFileName(string fileName)
        {
            var argException = new ArgumentException("El nombre del archivo no posee las convenciones correctas");
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileName.Trim()))
                throw argException;

            if (fileName.ToUpper().StartsWith("INCIDENTS"))
                return RequestTypeTarget.Soporte;

            if (fileName.ToUpper().StartsWith("SERVICE"))
                return RequestTypeTarget.Desarollo;

            throw argException;

        }
    }
}
