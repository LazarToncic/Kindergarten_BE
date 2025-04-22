using FluentValidation;
using Kindergarten.Application.Common.Dto.Parent;

namespace Kindergarten.Application.Common.Validators.Parent;

public class ParentRequestChildDtoValidator : AbstractValidator<ParentRequestChildDto>
{
    public ParentRequestChildDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(30).WithMessage("First name must be at most 30 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(30).WithMessage("Last name must be at most 30 characters long.");

        RuleFor(x => x.DateOfBirth)
            .Must(dob => dob < DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Allergies)
            .NotEmpty().When(x => x.HasAllergies)
            .WithMessage("Allergies list cannot be empty if the child has allergies.");

        RuleFor(x => x.MedicalConditions)
            .NotEmpty().When(x => x.HasMedicalIssues)
            .WithMessage("Medical conditions list cannot be empty if the child has medical issues.");
    }
}