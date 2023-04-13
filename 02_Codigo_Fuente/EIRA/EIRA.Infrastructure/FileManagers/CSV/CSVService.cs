using EIRA.Application.Extensions;
using EIRA.Application.Services.Files;
using System.Text;

namespace EIRA.Infrastructure.FileManagers.CSV
{
    public class CSVService : ICSVService
    {
        public string WriteCSV<T>(string fileName, string[] propertyNames, List<T> items = null) where T : class
        {
            if (items is null || !items.Any())
                return string.Empty;

            try
            {
                var headers = propertyNames.Select(x => PropertyExtension.GetReportHeader<T>(x));
                Type itemType = typeof(T);
                var diretorio = Path.GetTempPath();

                string path = $@"{diretorio}\{fileName}";

                using (var writer = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate), encoding: Encoding.UTF8))
                {
                    writer.WriteLine(string.Join("|", headers));

                    if (items.Any())
                    {
                        foreach (var item in items)
                        {
                            var columnValue = propertyNames.Select(propName => item?.GetType()?.GetProperty(propName)?.GetValue(item)?.ToString() ?? string.Empty);

                            writer.WriteLine(string.Join("|", columnValue));
                        }
                    }
                }
                var xlFile = new FileInfo(path);
                return xlFile.FullName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
