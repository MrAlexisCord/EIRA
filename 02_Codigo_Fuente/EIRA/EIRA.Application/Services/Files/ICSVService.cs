namespace EIRA.Application.Services.Files
{
    public interface ICSVService
    {
        string WriteCSV<T>(string fileName, string[] propertyNames, List<T> items = null) where T : class;
    }
}
