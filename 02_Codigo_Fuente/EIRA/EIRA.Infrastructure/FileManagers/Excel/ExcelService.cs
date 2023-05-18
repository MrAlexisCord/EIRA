using ClosedXML.Excel;
using EIRA.Application.Extensions;
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

                                var rowCelll = row.Cell(columnIndex);

                                if (cellValue != null && cellValue.Trim() != string.Empty)
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

        public string WriteExcel<T>(List<T> items, string[] propertyNames, string fileName, string sheetName = null) where T : class
        {
            if (items is null || !items.Any())
                return string.Empty;

            sheetName ??= "Hoja 1";
            try
            {
                var headers = propertyNames.Select(x => PropertyExtension.GetReportHeader<T>(x));
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(sheetName);

                    foreach (var header in headers.Select((head, index) => new { Name = head, ColumnIndex = index + 1 }))
                    {
                        worksheet.Cell(row: 1, column: header.ColumnIndex).Value = header.Name;
                        worksheet.Cell(row: 1, column: header.ColumnIndex).Style.Fill.BackgroundColor = XLColor.FromArgb(0xBFBFBF);
                        worksheet.Range(firstCellRow: 1, firstCellColumn: 1, lastCellRow: 1, lastCellColumn: headers.Count()).SetAutoFilter();
                    }

                    int rowIndex = 2;
                    foreach (var item in items)
                    {
                        foreach (var property in propertyNames.Select((prop, index) => new { Name = prop, ColumnIndex = index + 1 }))
                        {
                            AddValueToCell(item, worksheet, property.Name, rowIndex, property.ColumnIndex);
                        }
                        rowIndex++;
                    }
                    worksheet.Columns().AdjustToContents();

                    var diretorio = Path.GetTempPath();
                    string path = $@"{diretorio}\{fileName}";

                    workbook.SaveAs(path);

                    return path;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void AddValueToCell<T>(T item, IXLWorksheet worksheet, string propertyName, int rowIndex, int columnIndex) where T : class
        {
            var prop = typeof(T).GetProperty(propertyName);
            var propertyValue = prop.GetValue(item, null);
            if (propertyValue != null)
            {
                var cellValue = XLCellValue.FromObject(propertyValue);
                if (!(cellValue.IsDateTime && cellValue.GetDateTime().Year < 1900))
                {
                    worksheet.Cell(row: rowIndex, column: columnIndex).SetValue(cellValue);
                }
            }
        }
    }
}
