using EIRA.Application.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace EIRA.Application.Extensions
{
    public static class PropertyExtension
    {
        public static string GetReportHeader<T>(string propertyName)
        {
            var columnName = typeof(T)?.GetMember(propertyName)?.FirstOrDefault()?.GetCustomAttributesData()?
                            .Where(x => x?.AttributeType?.Name == nameof(ReportHeaderAttribute))?.FirstOrDefault()?
                            .ConstructorArguments?[0].Value?.ToString() ?? null;
            return columnName;
        }


        public static string GetReportHeaderWithFallBack<T>(string propertyName)
        {
            var columnName = typeof(T)?.GetMember(propertyName)?.FirstOrDefault()?.GetCustomAttributesData()?
                            .Where(x => x?.AttributeType?.Name == nameof(ReportHeaderAttribute))?.FirstOrDefault()?
                            .ConstructorArguments?[0].Value?.ToString() ??
                            (typeof(T)?.GetProperty(propertyName)?.GetCustomAttributes(typeof(DisplayAttribute), false)?.FirstOrDefault() as DisplayAttribute)?.Name ??
                            string.Empty;
            return columnName;
        }

        public static Dictionary<string, string> GetReportHeadersDictionary<T>()
        {
            var propertyNameList = typeof(T).GetProperties()?.Select(x => x.Name)?.ToList();

            if (!propertyNameList.Any())
                throw new ArgumentException(message: "Object does not contain properties");

            var headerDictionary = new Dictionary<string, string>();

            propertyNameList.ForEach(propertyName =>
            {
                var columnName = GetReportHeader<T>(propertyName);

                if (columnName is not null && !string.IsNullOrEmpty(columnName.Trim()))
                {
                    columnName = columnName.Trim();

                    // propertyName is the name of the property in the object (Id, Description, Name)
                    // columnName is the value setted in the custom attribute ReportHeader (ID, DESCRIPTION, NAME)
                    headerDictionary[propertyName] = columnName;
                }
            });

            if (headerDictionary is null || headerDictionary.Count() == 0)
                throw new Exception(message: "It was not possible to get properties dictionary");

            return headerDictionary;
        }
    }
}
