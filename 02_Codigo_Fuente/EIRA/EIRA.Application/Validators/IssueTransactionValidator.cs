using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Statics.Operations;

namespace EIRA.Application.Validators
{
    public static class IssueTransactionValidator
    {
        public static int FieldCreateValidation(this IssueCreateRequest body, List<JiraUploadIssueErrorLog> logsError)
        {
            var errors = 0;
            var campos = new List<string>();
            if (string.IsNullOrEmpty(body.Frente?.Value.Trim()))
            {
                campos.Add("Frente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.Compania?.Value.Trim()))
            {
                campos.Add("Compañia");
                errors++;
            }

            if (string.IsNullOrEmpty(body.ResponsableCliente?.Value.Trim()) || body.ResponsableCliente?.Value == "Responsable no asignado")
            {
                campos.Add("Responsable Cliente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.DescripcionEstadoCliente?.Content?.FirstOrDefault()?.Content.FirstOrDefault()?.Text?.Trim()))
            {
                campos.Add("Descripción Estado Cliente");
                errors++;
            }

            if (!body.FechaApertura.HasValue)
            {
                campos.Add("Fecha de apertura (registro)");
                errors++;
            }

            if (string.IsNullOrEmpty(body.NumeroAranda?.Trim()))
            {
                campos.Add("Número de caso");
                errors++;
            }

            if (errors > 0)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    NumeroAranda = body.NumeroAranda,
                    ErrorMessage = $"Los siguientes campos no deben estar vacíos: [{string.Join(",", campos)}]",
                    Operation = CrudOperations.CREATE
                });
            }


            return errors;

        }

        public static int FieldUpdateValidation(this IssueUpdateRequest body, List<JiraUploadIssueErrorLog> logsError)
        {
            var errors = 0;
            var campos = new List<string>();
            if (string.IsNullOrEmpty(body.Frente?.Value.Trim()))
            {
                campos.Add("Frente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.Compania?.Value.Trim()))
            {
                campos.Add("Compañía");
                errors++;
            }

            if (string.IsNullOrEmpty(body.ResponsableCliente?.Value.Trim()))
            {
                campos.Add("Responsable Cliente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.DescripcionEstadoCliente?.Content?.FirstOrDefault()?.Content.FirstOrDefault()?.Text?.Trim()))
            {
                campos.Add("Descripción Estado Cliente");
                errors++;
            }

            if (!body.FechaApertura.HasValue)
            {
                campos.Add("Fecha de apertura (registro)");
                errors++;
            }

            if (string.IsNullOrEmpty(body.NumeroAranda?.Trim()))
            {
                campos.Add("Número de caso");
                errors++;
            }

            if (errors > 0)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    NumeroAranda = body.NumeroAranda,
                    ErrorMessage = $"Los siguientes campos no deben estar vacíos {string.Join(",", campos)}",
                    Operation = CrudOperations.UPDATE
                });
            }

            return errors;
        }
    }
}
