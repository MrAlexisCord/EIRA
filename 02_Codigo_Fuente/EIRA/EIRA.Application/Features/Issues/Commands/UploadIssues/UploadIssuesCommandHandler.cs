using EIRA.Application.Extensions;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Services.Files;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommandHandler : IRequestHandler<UploadIssuesCommand, List<IssuesIncomingFile>>
    {

        private readonly ILogger<UploadIssuesCommandHandler> _logger;
        private readonly IExcelService _excelService;

        public UploadIssuesCommandHandler(ILogger<UploadIssuesCommandHandler> logger, IExcelService excelService)
        {
            _logger = logger;
            _excelService = excelService;
        }

        public async Task<List<IssuesIncomingFile>> Handle(UploadIssuesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("10001: Start cast excel rows into a list...");
            
            var sheetName = "Select v_sandra_casos_dia";
            var headers= PropertyExtension.GetReportHeadersDictionary<IssuesIncomingFile>();
            var response = _excelService.ReadExcel<IssuesIncomingFile>(request.FileStream, sheetName, headers);
            
            _logger.LogTrace("10001: End process...");

            return response;
        }
    }
}
