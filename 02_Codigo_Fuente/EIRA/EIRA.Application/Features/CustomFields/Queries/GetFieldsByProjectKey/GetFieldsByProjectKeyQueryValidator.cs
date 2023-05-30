using FluentValidation;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsByProjectKey
{
    public class GetFieldsByProjectKeyQueryValidator : AbstractValidator<GetFieldsByProjectKeyQuery>
    {
        public GetFieldsByProjectKeyQueryValidator()
        {
            RuleFor(p => p.ProjectKey)
                .NotEmpty().WithMessage("El campo Proyecto es obligatorio")
                .NotNull().WithMessage("El campo Proyecto es obligatorio");
        }
    }
}
