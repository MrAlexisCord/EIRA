using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Extensions;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.Files;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommandHandler : IRequestHandler<UploadIssuesCommand, List<JiraUploadIssueErrorLog>>
    {

        private readonly ILogger<UploadIssuesCommandHandler> _logger;
        private readonly IExcelService _excelService;
        private readonly IIssuesJiraRepository _issuesJiraRepository;

        public UploadIssuesCommandHandler(ILogger<UploadIssuesCommandHandler> logger,
            IExcelService excelService
,
            IIssuesJiraRepository issuesJiraRepository)
        {
            _logger = logger;
            _excelService = excelService;
            _issuesJiraRepository = issuesJiraRepository;
        }

        public async Task<List<JiraUploadIssueErrorLog>> Handle(UploadIssuesCommand request, CancellationToken cancellationToken)
        {
            var logRespose = new List<JiraUploadIssueErrorLog>();
            _logger.LogInformation("10001: Start cast excel rows into a list...");

            var sheetName = "Select v_sandra_casos_dia";
            var headers = PropertyExtension.GetReportHeadersDictionary<IssuesIncomingFile>();
            var response = _excelService.ReadExcel<IssuesIncomingFile>(request.FileStream, sheetName, headers);

            var validIssuesList = response?.Where(x => 
            (!string.IsNullOrEmpty(x.NumeroCaso) && !string.IsNullOrEmpty(x.NumeroCaso.Trim()))
            && x.Grupo.ToUpper().Contains("OLSOFT"));

            if (validIssuesList is not null && validIssuesList.Any())
            {
                var res = await _issuesJiraRepository.PostIssues(validIssuesList.ToList());
                logRespose = res;
            }
            else
            {
                _logger.LogTrace("10001: Not valid issues found...");
            }
            _logger.LogTrace("10001: End process...");

            return logRespose;
        }
    }
}
