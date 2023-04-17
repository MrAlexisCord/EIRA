namespace EIRA.Application.Extensions
{
    public static class ValuesHelperExtension
    {
        public static List<T> ValueToOneItemList<T>(this T value)
        {
            return new List<T> { value };
        }
    }
}
