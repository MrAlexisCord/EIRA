using EIRA.Application.Features.CustomFields.Queries.GetFieldsFollowUpConfigurationByProjectKey;
using FluentValidation;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsFollowUpConfigurationByProjectKey
{
    public class GetFieldsFollowUpConfigurationByProjectKeyQueryValidator : AbstractValidator<GetFieldsFollowUpConfigurationByProjectKeyQuery>
    {
        public GetFieldsFollowUpConfigurationByProjectKeyQueryValidator()
        {
            RuleFor(p => p.ProjectKey)
                .NotEmpty().WithMessage("El campo Proyecto es obligatorio")
                .NotNull().WithMessage("El campo Proyecto es obligatorio");
        }
    }
}
