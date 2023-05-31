using EIRA.Domain.Common;
using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Domain.EIRAEntities.Bas;

/// <summary>
/// Configuración de los Proyectos
/// </summary>
public partial class BasProject : BaseEntity
{
    /// <summary>
    /// Identificador Único del Proyecto
    /// </summary>
    public string ProjectId { get; set; }

    /// <summary>
    /// Llave del Proyecto
    /// </summary>
    public string ProjectKey { get; set; }

    /// <summary>
    /// Nombre o Descripción del Proyecto
    /// </summary>
    public string Name { get; set; }


    public virtual ICollection<AppConfigurationFollowUpReport> AppConfigurationFollowUpReport { get; set; } = new List<AppConfigurationFollowUpReport>();

    public virtual ICollection<AppConfigurationGlobalReport> AppConfigurationGlobalReport { get; set; } = new List<AppConfigurationGlobalReport>();

    public virtual ICollection<AppConfigurationIssueType> AppConfigurationIssueType { get; set; } = new List<AppConfigurationIssueType>();

    public virtual ICollection<AppConfigurationLoadInformation> AppConfigurationLoadInformation { get; set; } = new List<AppConfigurationLoadInformation>();
}
