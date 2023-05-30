namespace EIRA.Domain.Common
{
    public class BaseHisEntity : BaseEntity
    {
        public DateTime FechaVigenciaInicio { get; set; }
        /// <summary>
        /// Fecha de Finalización de la Vigencia del Registro
        /// </summary>
        public DateTime FechaVigenciaFin { get; set; }
        /// <summary>
        /// Identificador Único de la Acción de Auditoria (INSERT, UPDATE y DELETE)
        /// </summary>
        public string AccionAuditoriaId { get; set; }
        /// <summary>
        /// Campos Modificados (Solo aplica cuando la acción de auditoria es UPDATE)
        /// </summary>
        public string CamposModificados { get; set; }
    }
}
