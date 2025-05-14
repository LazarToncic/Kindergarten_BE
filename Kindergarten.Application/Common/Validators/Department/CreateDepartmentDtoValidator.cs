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

        RuleFor(x => x.KindergartenId)
            .NotEmpty().WithMessage("Kindergarten ID is required.");

        RuleFor(x => x.MaximumCapacity)
            .NotEmpty().WithMessage("MaximumCapacity is required.");
    }
}