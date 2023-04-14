namespace EIRA.Application.Services.Files
{
    public interface ICSVService
    {
        string WriteCSV<T>(List<T> items, string[] propertyNames, string fileName, string separator = null) where T : class;
    }
}
