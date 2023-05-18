using EIRA.Application.Models.Files.Outgoing;
using MediatR;

namespace EIRA.Application.Features.Issues.Queries.GetReporteComentarios
{
    public class GetReporteComentariosQuery: IRequest<List<IssueConComentariosReport>>
    {
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

        public string ProjectId { get; set; }
        public List<string> StatusIds { get; set; }
    }
}
