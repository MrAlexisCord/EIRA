namespace EIRA.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = true)]
    public class MensajeAttribute : Attribute
    {
        public string Mensaje { get; }

        public MensajeAttribute(string mensaje)
        {
            Mensaje = mensaje;
        }
    }
}
