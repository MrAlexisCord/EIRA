using EIRA.Application.DTOs;
using EIRA.Application.Models.Files.Outgoing;
using MediatR;

namespace EIRA.Application.Features.Issues.Queries.GetReporteComentarios
{
    public class GetReporteComentariosQuery: IRequest<List<IssueConComentariosReport>>
    {
    }
}
