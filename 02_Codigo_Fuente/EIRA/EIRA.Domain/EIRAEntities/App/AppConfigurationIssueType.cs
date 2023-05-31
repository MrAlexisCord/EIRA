using EIRA.Domain.Common;
using EIRA.Domain.EIRAEntities.Bas;

namespace EIRA.Domain.EIRAEntities.App;

/// <summary>
/// Configuración de Issue Type por Proyecto
/// </summary>
public partial class AppConfigurationIssueType:BaseEntity
{
    /// <summary>
    /// Identificador Único del Proyecto
    /// </summary>
    public string ProjectId { get; set; }

    /// <summary>
    /// Identificador Único del Tipo de Incidente
    /// </summary>
    public int IssueTypeId { get; set; }

    /// <summary>
    /// Valor del campo del Tipo de Incidente
    /// </summary>
    public string FieldValueName { get; set; }


    public virtual BasIssueType IssueType { get; set; }

    public virtual BasProject Project { get; set; }
}
