using FluentValidation;
using Kindergarten.Application.Common.Dto.Kindergarten;

namespace Kindergarten.Application.Common.Validators.Kindergarten;

public class CreateKindergartenDtoValidator : AbstractValidator<CreateKindergartenDto>
{
    public CreateKindergartenDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kindergarten name is required.")
            .MaximumLength(100).WithMessage("Kindergarten name must not exceed 100 characters.")
            .MinimumLength(8).WithMessage("Kindergarten name must exceed 8 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(30).WithMessage("Address must not exceed 30 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(30).WithMessage("City must not exceed 30 characters.");

        RuleFor(x => x.ContactPhone)
            .NotEmpty().WithMessage("Contact phone is required.")
            .Matches(@"^(?:\+381|0)\s?6\d\s?\d{3,4}\s?\d{3}$")
            .WithMessage("Invalid phone number format. Use formats like +381601234567, +381 60 1234 567, 0601234567, or 060 1234 567.");

    }
}