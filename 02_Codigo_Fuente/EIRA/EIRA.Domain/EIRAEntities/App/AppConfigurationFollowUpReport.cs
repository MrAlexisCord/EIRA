using EIRA.Domain.Common;
using EIRA.Domain.EIRAEntities.Bas;

namespace EIRA.Domain.EIRAEntities.App;

/// <summary>
/// Configuración de los Campos por Proyecto para el Reporte de Seguimiento
/// </summary>
public partial class AppConfigurationFollowUpReport : BaseEntity
{
    /// <summary>
    /// Identificador Único del Proyecto
    /// </summary>
    public string ProjectId { get; set; }

    /// <summary>
    /// Identificador Único del Campo
    /// </summary>
    public string FieldId { get; set; }

    /// <summary>
    /// Orden
    /// </summary>
    public int OrderNumber { get; set; }

    public virtual BasField Field { get; set; }

    public virtual BasProject Project { get; set; }
}
