namespace EIRA.Application.Services.Files
{
    public interface IExcelService
    {
        List<T> ReadExcel<T>(Stream stream, Dictionary<string, string> headers, int? pageNumber = null) where T : new();
        string WriteExcel<T>(List<T> items, string[] propertyNames, string fileName, string sheetName = null) where T : class;
    }
}
