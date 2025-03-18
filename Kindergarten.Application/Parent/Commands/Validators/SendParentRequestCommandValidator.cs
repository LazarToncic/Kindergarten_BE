using FluentValidation;
using Kindergarten.Application.Common.Validators.Parent;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Parent.Commands.Validators;

public class SendParentRequestCommandValidator : AbstractValidator<SendParentRequestCommand>
{
    public SendParentRequestCommandValidator()
    {
        RuleFor(x => x.Dto.NumberOfChildren)
            .GreaterThan(0).WithMessage("At least one child must be provided.");

        RuleFor(x => x.Dto.PreferredKindergarten)
            .NotEmpty().WithMessage("Preferred kindergarten is required.")
            .MaximumLength(30).WithMessage("Preferred kindergarten must be at most 30 characters long.");

        RuleFor(x => x.Dto.ParentChildRelationship)
            .Must(value => Enum.IsDefined(typeof(ParentChildRelationship), value))
            .WithMessage("Parent child relationship is required and must be a valid value.");

        RuleForEach(x => x.Dto.Children)
            .SetValidator(new ParentRequestChildDtoValidator());
    }
}