using FluentValidation;

namespace EIRA.Application.Features.Issues.Queries.GetReporteComentarios
{
    public class GetReporteComentariosQueryValidator : AbstractValidator<GetReporteComentariosQuery>
    {
        public GetReporteComentariosQueryValidator()
        {
            //RuleFor(p => p.StartDate)
            //    .LessThanOrEqualTo(p => p.EndDate)
            //    .WithMessage("La fecha inicial deber menor o igual a la fecha final");

            RuleFor(p => p.ProjectId)
                .NotNull().WithMessage("Debe seleccionar un proyecto")
                .NotEmpty().WithMessage("Debe seleccionar un proyecto");

            RuleFor(p => p.StatusIds)
                .NotNull().WithMessage("Debe seleccionar al menos un estado")
                .Must(x => x != null && x.Any()).WithMessage("Debe seleccionar al menos un estado");
        }
    }
}
