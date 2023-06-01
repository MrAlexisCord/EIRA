using FluentValidation;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsGlobalConfigurationByProjectKey
{
    public class GetFieldsGlobalConfigurationByProjectKeyQueryValidator : AbstractValidator<GetFieldsGlobalConfigurationByProjectKeyQuery>
    {
        public GetFieldsGlobalConfigurationByProjectKeyQueryValidator()
        {
            RuleFor(p => p.ProjectKey)
                .NotEmpty().WithMessage("El campo Proyecto es obligatorio")
                .NotNull().WithMessage("El campo Proyecto es obligatorio");
        }
    }
}
