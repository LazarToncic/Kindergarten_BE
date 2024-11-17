using FluentValidation;
using Kindergarten.Application.Common.Dto.Auth;

namespace Kindergarten.Application.Common.Validators.Auth;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(20).WithMessage("First name must not exceed 20 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(20).WithMessage("Last name must not exceed 20 characters.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(6).WithMessage("Username must be at least 6 characters long.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
            .Matches(@"^\w+$").WithMessage("Username can only contain letters, numbers, and underscores.");

        RuleFor(x => x.YearOfBirth)
            .InclusiveBetween(1900, DateTime.UtcNow.Year - 18).WithMessage("You must be at least 18 years old.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=\?\<\>\[\]\{\}\|\~]").WithMessage("Password must contain at least one special character (!@#$%^&*()-+=?<>[]{}|~).");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(?:\+381|0)\s?6\d\s?\d{3,4}\s?\d{3}$")
            .WithMessage("Invalid phone number format. Use formats like +381601234567, +381 60 1234 567, 0601234567, or 060 1234 567.");
    }
}