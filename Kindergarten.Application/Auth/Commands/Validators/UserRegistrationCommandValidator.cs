using FluentValidation;
using Kindergarten.Application.Common.Validators.Auth;

namespace Kindergarten.Application.Auth.Commands.Validators;

public class UserRegistrationCommandValidator : AbstractValidator<UserRegistrationCommand>
{
    public UserRegistrationCommandValidator()
    {
        RuleFor(x => x.Dto)
            .SetValidator(new RegisterDtoValidator());
    }
}