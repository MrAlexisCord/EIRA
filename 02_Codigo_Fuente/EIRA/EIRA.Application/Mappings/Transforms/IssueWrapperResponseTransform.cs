using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Outgoing;

namespace EIRA.Application.Mappings.Transforms
{
    public static class IssueWrapperResponseTransform
    {
        public static IssueConComentariosReport FromIssueToIssueConComentariosReport(Issue issue, IEnumerable<string> issueCommentsFormatted)
        {
            issueCommentsFormatted = issueCommentsFormatted.Where(x => !string.IsNullOrEmpty(x));

            return new IssueConComentariosReport
            {
                Compania = issue?.Fields?.Compania?.Value ?? string.Empty,
                Complejidad = issue?.Fields?.Complejidad?.Value ?? string.Empty,
                Desarrollador = issue?.Fields?.Desarrollador?.DisplayName ?? string.Empty,
                DescripcionCorta = (issue?.Fields?.HistoriaUsuario?.Content?.FirstOrDefault()?.Content?.FirstOrDefault()?.Text ?? string.Empty).Replace("\n", Environment.NewLine),
                FechaCierre = issue?.Fields?.FechaCierre,
                FechaEntregaAnalisisN1 = issue?.Fields?.FechaEntregaAnalisisN1,
                FechaEntregaConstruccion = issue?.Fields?.FechaEntregaConstruccion,
                FechaEntregaPropuestaSolucion = issue?.Fields?.FechaEntregaPropuestaSolucion,
                //FechaEstimada = issue?.Fields?.FechaEstimada,
                NumeroCaso = issue?.Fields?.NumeroCaso ?? string.Empty,
                Observaciones = string.Join(Environment.NewLine, issueCommentsFormatted),
                Prioridad = issue?.Fields?.Prioridad,
                //Project = issue?.Fields?.Project?.Name ?? string.Empty,
                ResponsableCliente = issue?.Fields?.ResponsableCliente?.Value ?? string.Empty,
                //Tarea = issue?.Fields?.Tarea ?? string.Empty,
                Estado = issue?.Fields?.Status?.Name ?? string.Empty,

                // FUll Report
                EstadoCliente = (issue?.Fields?.DescripcionEstadoCliente?.Content?.FirstOrDefault()?.Content?.FirstOrDefault()?.Text ?? string.Empty).Replace("\n", Environment.NewLine),
                FechaAsignacion = issue?.Fields?.FechaAsignacion,
                FechaEstimadaConstruccion = issue?.Fields?.FechaEstimadaConstruccion,
                FechaEstimadaPropuestaSolucion = issue?.Fields?.FechaEstimadaPropuestaSolucion,
                FechaRegistro = issue?.Fields?.FechaApertura,
                TiempoEstimadoConstruccion = issue?.Fields?.TiempoEstimadoConstruccion,
                TiempoEstimadoPropuestaSolucion = issue?.Fields?.TiempoEstimadoPropuestaSolucion,
                TiempoEstimadoSoportePruebas = issue?.Fields?.TiempoEstimadoSoportePruebas,

            };
        }
    }
}
