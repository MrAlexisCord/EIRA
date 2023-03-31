using System.Reflection;

namespace EIRA.Application.Attributes.Helpers
{
    public static class ReportHeaderHelper
    {
        public static string GetHeaderValue(PropertyInfo property)
        {
            object[] customAttributes = property.GetCustomAttributes(typeof(ReportHeaderAttribute), inherit: false);
            if (customAttributes != null && customAttributes.Length != 0)
            {
                return ((ReportHeaderAttribute)customAttributes[0]).Value;
            }

            return null;
        }
    }
}
