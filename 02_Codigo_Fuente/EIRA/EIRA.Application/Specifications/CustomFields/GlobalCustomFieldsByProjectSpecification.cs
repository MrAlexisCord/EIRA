using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Application.Specifications.CustomFields
{
    public class GlobalCustomFieldsByProjectSpecification : BaseSpecification<AppConfigurationGlobalReport>
    {
        public GlobalCustomFieldsByProjectSpecification(string projectKey) : base(f => f.ProjectId.ToUpper() == projectKey.ToUpper())
        {
            ApplyOrderBy(x => x.OrderNumber);
        }

        public GlobalCustomFieldsByProjectSpecification(string projectKey, string fieldId) : base(f =>
        f.ProjectId.ToUpper() == projectKey.ToUpper()
        && f.FieldId.ToUpper() == fieldId.ToUpper()
        )
        {
            ApplyOrderBy(x => x.OrderNumber);
        }
    }
}
