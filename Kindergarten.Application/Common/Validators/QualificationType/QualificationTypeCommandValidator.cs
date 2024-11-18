using FluentValidation;
using Kindergarten.Application.QualificationType.Commands;

namespace Kindergarten.Application.Common.Validators.QualificationType;

public class QualificationTypeCommandValidator : AbstractValidator<QualificationTypeCommand>
{
    public QualificationTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cant be empty");
    }
}