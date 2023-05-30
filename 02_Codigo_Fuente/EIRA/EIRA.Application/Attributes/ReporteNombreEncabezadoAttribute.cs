namespace EIRA.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ReporteNombreEncabezadoAttribute : Attribute
    {
        public string Value { get; }

        public string Key { get; }

        public ReporteNombreEncabezadoAttribute(string cellName, string cellKey = null)
        {
            Value = cellName;
            Key = cellKey;
        }
    }
}
