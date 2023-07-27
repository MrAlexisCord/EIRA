using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Outgoing;

namespace EIRA.Application.Mappings.Transforms
{
    public static class IssueWrapperResponseTransform
    {
        public static IssueConComentariosReport FromIssueToIssueConComentariosReport(Issue issue, IEnumerable<string> issueCommentsFormatted)
        {
            issueCommentsFormatted = issueCommentsFormatted.Where(x => !string.IsNullOrEmpty(x));

            var commentsString = issueCommentsFormatted is null || !issueCommentsFormatted.Any()
                ? string.Empty
                : string.Join(Environment.NewLine, issueCommentsFormatted);

            var reporteString = issue?.Fields?.Reporte is null || !issue.Fields.Reporte.Any()
                ? string.Empty
                : string.Join(Environment.NewLine, issue?.Fields?.Reporte?.Select(x => x.Value));

            var responsablesMultiplesString = issue?.Fields?.ResponsablesMultiples is null || !issue.Fields.ResponsablesMultiples.Any()
                ? string.Empty
                : string.Join(Environment.NewLine, issue?.Fields?.ResponsablesMultiples?.Select(x => x.Value));

            var responsablesMultiplesTripleaSUIString = issue?.Fields?.ResponsablesMultiplesTripleaSUI is null || !issue.Fields.ResponsablesMultiplesTripleaSUI.Any()
                ? string.Empty
                : string.Join(Environment.NewLine, issue?.Fields?.ResponsablesMultiplesTripleaSUI?.Select(x => x.Value));

            var responsablesMultiplesTripleaCartasString = issue?.Fields?.ResponsablesMultiplesTripleaCartas is null || !issue.Fields.ResponsablesMultiplesTripleaCartas.Any()
               ? string.Empty
               : string.Join(Environment.NewLine, issue?.Fields?.ResponsablesMultiplesTripleaCartas?.Select(x => x.Value));

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
                Observaciones = commentsString,
                Prioridad = issue?.Fields?.Prioridad,
                //Project = issue?.Fields?.Project?.Name ?? string.Empty,
                ResponsableCliente = issue?.Fields?.ResponsableCliente?.Value ?? string.Empty,
                //Tarea = issue?.Fields?.Tarea ?? string.Empty,
                Estado = issue?.Fields?.Status?.Name ?? string.Empty,

                // Full Report
                EstadoCliente = (issue?.Fields?.DescripcionEstadoCliente?.Content?.FirstOrDefault()?.Content?.FirstOrDefault()?.Text ?? string.Empty).Replace("\n", Environment.NewLine),
                FechaAsignacion = issue?.Fields?.FechaAsignacion,
                FechaEstimadaConstruccion = issue?.Fields?.FechaEstimadaConstruccion,
                FechaEstimadaPropuestaSolucion = issue?.Fields?.FechaEstimadaPropuestaSolucion,
                FechaRegistro = issue?.Fields?.FechaApertura,
                TiempoEstimadoConstruccion = issue?.Fields?.TiempoEstimadoConstruccion,
                TiempoEstimadoPropuestaSolucion = issue?.Fields?.TiempoEstimadoPropuestaSolucion,
                TiempoEstimadoSoportePruebas = issue?.Fields?.TiempoEstimadoSoportePruebas,

                // NUEVOS
                FechaSolucion = issue?.Fields?.FechaSolucion,
                Frente = issue?.Fields?.Frente?.Value ?? string.Empty,
                Issuetype = issue?.Fields?.Issuetype?.Name ?? string.Empty,
                Project = issue?.Fields?.Project?.Key ?? string.Empty,
                Reporte = reporteString,
                ResponsablesMultiples = responsablesMultiplesString,
                ResponsablesMultiplesTripleaSUI = responsablesMultiplesTripleaSUIString,
                ResponsablesMultiplesTripleaCARTAS = responsablesMultiplesTripleaCartasString,
                Summary = issue?.Fields?.Summary ?? string.Empty,

                // TIME TO
                TimeToAttention = issue?.Fields?.TimeToAttention?.CompletedCycles?.FirstOrDefault()?.BreachTime?.Friendly ?? string.Empty,
                TimeToResolution = issue?.Fields?.TimeToResolution?.CompletedCycles?.FirstOrDefault()?.BreachTime?.Friendly ?? string.Empty,
                TiempoEntregaAnalisis = issue?.Fields?.TiempoEntregaAnalisis?.CompletedCycles?.FirstOrDefault()?.BreachTime?.Friendly ?? string.Empty,
            };
        }
    }
}
