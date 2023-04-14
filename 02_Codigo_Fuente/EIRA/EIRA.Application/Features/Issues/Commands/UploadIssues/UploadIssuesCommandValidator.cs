using FluentValidation;

namespace EIRA.Application.Features.Issues.Commands.UploadIssues
{
    public class UploadIssuesCommandValidator : AbstractValidator<UploadIssuesCommand>
    {
        public UploadIssuesCommandValidator()
        {
            RuleFor(p => p.FileStream)
                .NotNull().WithMessage("Debe seleccionar un archivo");
        }
    }
}
