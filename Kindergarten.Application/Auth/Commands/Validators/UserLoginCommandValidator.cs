using FluentValidation;
using Kindergarten.Application.Common.Validators.Auth;

namespace Kindergarten.Application.Auth.Commands.Validators;

public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(x => x.Dto)
            .SetValidator(new LoginUserDtoValidator());
    }
}