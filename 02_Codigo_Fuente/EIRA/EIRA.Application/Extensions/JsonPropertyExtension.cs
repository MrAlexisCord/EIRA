using EIRA.Application.Attributes;
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


        public static List<string> GetHeadersByFieldNames<T>(this List<string> jsonProperties)
        {
            var result = new List<string>();
            try
            {
                var type = typeof(T);
                var properties = type.GetProperties();

                foreach (var jsonPropertyName in jsonProperties)
                {
                    var propertyJson = properties?.Where(x => (x.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? string.Empty) == jsonPropertyName)?.FirstOrDefault();

                    if (propertyJson is not null)
                    {
                        //var headerValue = propertyJson.GetCustomAttribute<ReportHeaderAttribute>()?.Value ?? propertyJson.Name ?? string.Empty;
                        var headerValue = propertyJson.Name;
                        result.Add(headerValue);
                    }
                }

                return result;

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
