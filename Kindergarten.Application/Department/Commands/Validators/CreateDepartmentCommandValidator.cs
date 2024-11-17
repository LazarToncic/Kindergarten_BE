using FluentValidation;
using Kindergarten.Application.Common.Validators.Department;

namespace Kindergarten.Application.Department.Commands.Validators;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Dto)
            .SetValidator(new CreateDepartmentDtoValidator());
    }
}