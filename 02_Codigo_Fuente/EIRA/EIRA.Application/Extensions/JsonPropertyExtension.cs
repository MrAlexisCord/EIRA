using Newtonsoft.Json;
using System.Reflection;

namespace EIRA.Application.Extensions
{
    public static class JsonPropertyExtension
    {
        public static object GetPropertyInfoByJsonPropertyName(this object obj, string jsonPropertyName)
        {
            try
            {
                var type = obj.GetType();
                var properties = type.GetProperties();
                var propertyJson = properties?.Where(x => (x.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? string.Empty) == jsonPropertyName)?.FirstOrDefault();
                if (propertyJson is null)
                    return null;

                return propertyJson.GetValue(obj);
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
