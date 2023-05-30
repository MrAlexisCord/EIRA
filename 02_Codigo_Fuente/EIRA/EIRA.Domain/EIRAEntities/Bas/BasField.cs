using EIRA.Domain.Common;
using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Domain.EIRAEntities.Bas;

/// <summary>
/// Configuración de los campos
/// </summary>
public partial class BasField : BaseEntity
{
    /// <summary>
    /// Identificador Único del Campo
    /// </summary>
    public string FieldId { get; set; }

    /// <summary>
    /// Llave del Campo
    /// </summary>
    public string FieldKey { get; set; }

    /// <summary>
    /// Nombre o Descripción del Campo
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Tipo del Campo
    /// </summary>
    public string FieldType { get; set; }

    public virtual ICollection<AppConfigurationFollowUpReport> AppConfigurationFollowUpReport { get; set; } = new List<AppConfigurationFollowUpReport>();

    public virtual ICollection<AppConfigurationGlobalReport> AppConfigurationGlobalReport { get; set; } = new List<AppConfigurationGlobalReport>();

    public virtual ICollection<AppConfigurationLoadInformation> AppConfigurationLoadInformation { get; set; } = new List<AppConfigurationLoadInformation>();
}
