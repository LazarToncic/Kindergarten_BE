using FluentValidation;
using Kindergarten.Application.Parent.Commands;

namespace Kindergarten.Application.Common.Validators.Parent;

public class ApproveParentRequestOnlineCommandValidator : AbstractValidator<ApproveParentRequestOnlineCommand>
{
    public ApproveParentRequestOnlineCommandValidator()
    {
        RuleFor(x => x.ParentRequestId).NotEmpty();
    }
}