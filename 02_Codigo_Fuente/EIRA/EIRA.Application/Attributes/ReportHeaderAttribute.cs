namespace EIRA.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ReportHeaderAttribute: Attribute
    {
        public string Value { get; }

        public string Key { get; }

        public ReportHeaderAttribute(string cellName, string cellKey = null)
        {
            Value = cellName;
            Key = cellKey;
        }
    }
}
