namespace EIRA.Application.Extensions
{
    public static class DateFormatExtension
    {
        public static string ExportableDateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy hh-mm-ss");
        }

        public static string ExportableDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }
    }
}
