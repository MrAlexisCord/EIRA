namespace EIRA.Application.Services.Files
{
    /// <summary>
    /// EXCEL File Manager Service
    /// <dependency>ClosedXML: Version 0.100.3</dependency>
    /// <author>Alexis Córdoba</author>
    /// <message>Amat Victoria Curam </message>
    /// </summary>
    public interface IExcelService
    {
        List<T> ReadExcel<T>(Stream stream, string sheetName, string[] headers) where T : new();
    }
}
