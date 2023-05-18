using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Models.Files.Outgoing;
using MediatR;

namespace EIRA.Application.Features.Issues.Queries.GetReporteComentarios
{
    public class GetReporteComentariosQueryHandler : IRequestHandler<GetReporteComentariosQuery, List<IssueConComentariosReport>>
    {
        private readonly IIssuesJiraRepository _issuesJiraRepository;

        public GetReporteComentariosQueryHandler(IIssuesJiraRepository issuesJiraRepository)
        {
            _issuesJiraRepository = issuesJiraRepository;
        }

        public async Task<List<IssueConComentariosReport>> Handle(GetReporteComentariosQuery request, CancellationToken cancellationToken)
        {
            var response = await _issuesJiraRepository.GetIssuesByProjectId(projectId: request.ProjectId, request.StatusIds);
            return response;
        }
    }
}
