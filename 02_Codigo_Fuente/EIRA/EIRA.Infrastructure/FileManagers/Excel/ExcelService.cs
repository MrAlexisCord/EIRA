using ClosedXML.Excel;
using EIRA.Application.Services.Files;

namespace EIRA.Infrastructure.FileManagers.Excel
{
    public class ExcelService : IExcelService
    {
        public List<T> ReadExcel<T>(Stream stream, string sheetName, Dictionary<string, string> headers) where T : new()
        {
            var list = new List<T>();

            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(sheetName);
                var headerRow = worksheet.Row(1);
                var headerColumns = new Dictionary<string, int>();

                // Find colmun indexes from specified headers
                for (int i = 1; i <= headerRow.CellsUsed().Count(); i++)
                {
                    var cellValue = headerRow.Cell(i).Value.ToString();
                    if (headers.Select(x => x.Value).Contains(cellValue))
                    {
                        headerColumns[cellValue] = i;
                    }
                }

                // Iterate rows for build the list of objects
                for (int i = 2; i <= worksheet.Rows().Count(); i++)
                {
                    var obj = new T();
                    var row = worksheet.Row(i);

                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if (headerColumns.TryGetValue(headers[prop.Name], out int columnIndex))
                        {
                            var cellValue = row.Cell(columnIndex).GetString();

                            if (cellValue != null)
                            {
                                prop.SetValue(obj, Convert.ChangeType(cellValue, prop.PropertyType));
                            }
                        }
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
