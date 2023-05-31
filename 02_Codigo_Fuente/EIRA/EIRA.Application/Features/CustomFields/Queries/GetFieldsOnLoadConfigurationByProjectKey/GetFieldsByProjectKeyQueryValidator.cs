using FluentValidation;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsOnLoadConfigurationByProjectKey
{
    public class GetFieldsOnLoadConfigurationByProjectKeyQueryValidator : AbstractValidator<GetFieldsOnLoadConfigurationByProjectKeyQuery>
    {
        public GetFieldsOnLoadConfigurationByProjectKeyQueryValidator()
        {
            RuleFor(p => p.ProjectKey)
                .NotEmpty().WithMessage("El campo Proyecto es obligatorio")
                .NotNull().WithMessage("El campo Proyecto es obligatorio");
        }
    }
}
