using FluentValidation;
using Kindergarten.Application.Parent.Commands;

namespace Kindergarten.Application.Common.Validators.Parent;

public class ApproveParentRequestInPersonCommandValidator : AbstractValidator<ApproveParentRequestInPersonCommand>
{
    public ApproveParentRequestInPersonCommandValidator()
    {
        RuleFor(x => x.ParentRequestId).NotEmpty();
    }
}