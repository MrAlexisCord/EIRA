using ClosedXML.Excel;
using EIRA.Application.Services.Files;

namespace EIRA.Infrastructure.FileManagers.Excel
{
    public class ExcelService : IExcelService
    {
        public List<T> ReadExcel<T>(Stream stream, Dictionary<string, string> headers, int? pageNumber = null) where T : new()
        {
            var list = new List<T>();

            pageNumber ??= 1;

            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
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
                        try
                        {
                            if (headerColumns.TryGetValue(headers[prop.Name], out int columnIndex))
                            {
                                var cellValue = row.Cell(columnIndex).GetString();

                                if (cellValue != null)
                                {
                                    var convertionType = Nullable.GetUnderlyingType(prop.PropertyType);
                                    prop.SetValue(obj, Convert.ChangeType(cellValue, convertionType is not null ? convertionType : prop.PropertyType));
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            //throw;
                        }
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
