namespace EIRA.Application.Services.Files
{
    public interface IExcelService
    {
        List<T> ReadExcel<T>(Stream stream, string sheetName, Dictionary<string, string> headers) where T : new();
    }
}
