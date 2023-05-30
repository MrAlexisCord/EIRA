using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace EIRA.Application.Attributes.Helpers
{
    public static class Conversion
    {
        public static string GetNombreTabla<T>() where T : class
        {
            Type typeFromHandle = typeof(T);
            object[] customAttributes = typeFromHandle.GetCustomAttributes(typeof(TableAttribute), inherit: false);
            if (customAttributes != null && customAttributes.Length != 0)
            {
                return ((TableAttribute)customAttributes[0]).Name;
            }

            return null;
        }

        public static string GetNombreColumna(object property)
        {
            object[] customAttributes = (property as FieldInfo).GetCustomAttributes(typeof(ColumnAttribute), inherit: false);
            if (customAttributes != null && customAttributes.Length != 0)
            {
                return ((ColumnAttribute)customAttributes[0]).Name;
            }

            return (property as FieldInfo).Name;
        }

        //public static string GetRutaWebSocket<T>(T servicio) where T : Hub
        //{
        //    Type type = ((object)servicio).GetType();
        //    MemberInfo[] member = type.GetMember(((object)servicio).ToString());
        //    if (member != null && member.Length != 0)
        //    {
        //        object[] customAttributes = member[0].GetCustomAttributes(typeof(ServicioWebSocketAttribute), inherit: false);
        //        if (customAttributes != null && customAttributes.Length != 0)
        //        {
        //            return ((ServicioWebSocketAttribute)customAttributes[0]).Route;
        //        }
        //    }

        //    return null;
        //}

        public static string GetMensaje(Enum @enum)
        {
            Type type = @enum.GetType();
            MemberInfo[] member = type.GetMember(@enum.ToString());
            if (member != null && member.Length != 0)
            {
                object[] customAttributes = member[0].GetCustomAttributes(typeof(MensajeAttribute), inherit: false);
                if (customAttributes != null && customAttributes.Length != 0)
                {
                    return ((MensajeAttribute)customAttributes[0]).Mensaje;
                }
            }

            return Enum.GetName(@enum.GetType(), @enum);
        }

        public static byte[] FileToByteArray(string fileName)
        {
            try
            {
                //byte[] array = null;
                using FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                using BinaryReader binaryReader = new BinaryReader(input);
                long length = new FileInfo(fileName).Length;
                return binaryReader.ReadBytes((int)length);
            }
            catch
            {
                return null;
            }
        }

        public static string GetNombreEncabezado(PropertyInfo property)
        {
            object[] customAttributes = property.GetCustomAttributes(typeof(ReporteNombreEncabezadoAttribute), inherit: false);
            if (customAttributes != null && customAttributes.Length != 0)
            {
                return ((ReporteNombreEncabezadoAttribute)customAttributes[0]).Value;
            }

            return null;
        }

        public static string GetLlaveEncabezado(PropertyInfo property)
        {
            object[] customAttributes = property.GetCustomAttributes(typeof(ReporteNombreEncabezadoAttribute), inherit: false);
            if (customAttributes != null && customAttributes.Length != 0)
            {
                string key = ((ReporteNombreEncabezadoAttribute)customAttributes[0]).Key;
                return string.IsNullOrEmpty(key) ? property.Name : key;
            }

            return null;
        }
    }
}
