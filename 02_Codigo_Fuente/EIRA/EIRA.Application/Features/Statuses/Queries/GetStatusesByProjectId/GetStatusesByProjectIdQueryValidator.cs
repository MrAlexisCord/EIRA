using FluentValidation;

namespace EIRA.Application.Features.Statuses.Queries.GetStatusesByProjectId
{
    public class GetStatusesByProjectIdQueryValidator: AbstractValidator<GetStatusesByProjectIdQuery>  
    {
        public GetStatusesByProjectIdQueryValidator()
        {
            RuleFor(p => p.ProjectId)
                .NotNull().WithMessage("Debe ingresar el proyecto")
                .NotEmpty().WithMessage("Debe ingresar el proyecto");
        }
    }
}
