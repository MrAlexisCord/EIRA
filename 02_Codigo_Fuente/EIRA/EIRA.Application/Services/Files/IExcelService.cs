namespace EIRA.Application.Services.Files
{
    public interface IExcelService
    {
        List<T> ReadExcel<T>(Stream stream, Dictionary<string, string> headers, int? pageNumber = null) where T : new();
    }
}
