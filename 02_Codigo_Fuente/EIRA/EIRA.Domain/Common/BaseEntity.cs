namespace EIRA.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// ¿Esta Activo? (1-True, 0-False)
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Fecha de Creación del Registro
        /// </summary>
        public DateTime CreationAt { get; set; }

        /// <summary>
        /// Usuario que Realizó la Creación del Registro
        /// </summary>
        public string UserAt { get; set; }

        /// <summary>
        /// Fecha de la Última Actualización del Registro
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Usuario que Realizó la Última Actualización del Registro
        /// </summary>
        public string UpdateUser { get; set; }
    }
}
