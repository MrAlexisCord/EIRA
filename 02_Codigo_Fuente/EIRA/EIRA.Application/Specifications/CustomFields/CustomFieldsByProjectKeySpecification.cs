using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Application.Specifications.CustomFields
{
    public class CustomFieldsByProjectKeySpecification : BaseSpecification<AppConfigurationLoadInformation>
    {
        public CustomFieldsByProjectKeySpecification(string projectKey) : base(f => f.ProjectId.ToUpper() == projectKey.ToUpper())
        {
            ApplyOrderBy(x => x.OrderNumber);
        }

        public CustomFieldsByProjectKeySpecification(string projectKey, string fieldId) : base(f =>
        f.ProjectId.ToUpper() == projectKey.ToUpper()
        && f.FieldId.ToUpper() == fieldId.ToUpper() 
        )
        {
            ApplyOrderBy(x => x.OrderNumber);
        }
    }
}
