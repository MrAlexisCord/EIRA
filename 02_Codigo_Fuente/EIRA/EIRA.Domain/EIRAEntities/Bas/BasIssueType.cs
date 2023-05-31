using EIRA.Domain.Common;
using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Domain.EIRAEntities.Bas;

/// <summary>
/// Configuración de los Proyectos
/// </summary>
public partial class BasIssueType : BaseEntity
{
    /// <summary>
    /// Identificador Único del Tipo de Incidente
    /// </summary>
    public int IssueTypeId { get; set; }

    /// <summary>
    /// Nombre o descripción del Tipo de Incidente
    /// </summary>
    public string IssueTypeName { get; set; }


    public virtual ICollection<AppConfigurationIssueType> AppConfigurationIssueType { get; set; } = new List<AppConfigurationIssueType>();
}
