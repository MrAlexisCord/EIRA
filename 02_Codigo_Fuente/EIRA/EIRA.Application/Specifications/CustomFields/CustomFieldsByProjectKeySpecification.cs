using EIRA.Domain.EIRAEntities.App;

namespace EIRA.Application.Specifications.CustomFields
{
    public class CustomFieldsByProjectKeySpecification : BaseSpecification<AppConfigurationLoadInformation>
    {
        public CustomFieldsByProjectKeySpecification(string projectKey) : base(f => f.ProjectId.ToUpper() == projectKey.ToUpper())
        {

        }
    }
}
