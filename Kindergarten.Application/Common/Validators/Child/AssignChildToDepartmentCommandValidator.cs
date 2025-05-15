using FluentValidation;
using Kindergarten.Application.Child.Commands;

namespace Kindergarten.Application.Common.Validators.Child;

public class AssignChildToDepartmentCommandValidator : AbstractValidator<AssignChildToDepartmentCommand>
{
    public AssignChildToDepartmentCommandValidator()
    {
        RuleFor(x => x.ChildId)
            .NotEmpty()
            .WithMessage("Child Id cannot be empty");
        
        RuleFor(x => x.DepartmentId)
            .NotEmpty()
            .WithMessage("Department Id cannot be empty");
    }
}