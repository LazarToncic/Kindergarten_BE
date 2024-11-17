using FluentValidation;
using Kindergarten.Application.Common.Dto.Department;

namespace Kindergarten.Application.Common.Validators.Department;

public class CreateDepartmentDtoValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .MaximumLength(10).WithMessage("Department name must not exceed 10 characters.");

        RuleFor(x => x.AgeGroup)
            .InclusiveBetween(0, 7).WithMessage("Age group must be between 1 and 7.");

        RuleFor(x => x.KindergartenName)
            .NotEmpty().WithMessage("Kindergarten name is required.")
            .MaximumLength(30).WithMessage("Kindergarten name must not exceed 30 characters.");
    }
}