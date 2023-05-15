using EIRA.Application.Models.Files.Outgoing;
using MediatR;

namespace EIRA.Application.Features.Issues.Queries.GetReporteComentarios
{
    public class GetReporteComentariosQueryHandler : IRequestHandler<GetReporteComentariosQuery, List<IssueConComentariosReport>>
    {
        public async Task<List<IssueConComentariosReport>> Handle(GetReporteComentariosQuery request, CancellationToken cancellationToken)
        {
            return new List<IssueConComentariosReport>
            {
                new IssueConComentariosReport{Entero=12345, EnteroNullable=23456, Fecha=DateTime.UtcNow, FechaNullable=DateTime.UtcNow, IssueKeyOrId="SE-10", NumeroAranda="66666"},
                new IssueConComentariosReport{Entero=12345, EnteroNullable=null, Fecha=DateTime.UtcNow, FechaNullable=null, IssueKeyOrId="SE-10", NumeroAranda=null},
                new IssueConComentariosReport{Entero=12345, EnteroNullable=23456, Fecha=DateTime.UtcNow, FechaNullable=DateTime.UtcNow, IssueKeyOrId=null, NumeroAranda="66666"},
                new IssueConComentariosReport{Entero=12345, EnteroNullable=null, Fecha=DateTime.UtcNow, FechaNullable=DateTime.UtcNow, IssueKeyOrId="SE-10", NumeroAranda="66666"},
                new IssueConComentariosReport{Entero=12345, EnteroNullable=23456, Fecha=DateTime.UtcNow, FechaNullable=null, IssueKeyOrId=null, NumeroAranda="66666"},
                new IssueConComentariosReport(),
            };
        }
    }
}
