using FluentValidation;
using Kindergarten.Application.Common.Dto.Auth;

namespace Kindergarten.Application.Common.Validators.Auth;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email or Username is required.")
            .Must(BeAValidEmailOrUsername).WithMessage("Enter a valid email or username.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[!@#$%^&*(),.?\\[\]{}|<>]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.RememberMe)
            .NotNull().WithMessage("RememberMe must be specified.");
    }

    private bool BeAValidEmailOrUsername(string input)
    {
        var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

        var usernameRegex = new System.Text.RegularExpressions.Regex(@"^\w{6,20}$");

        return emailRegex.IsMatch(input) || usernameRegex.IsMatch(input);
    }
}