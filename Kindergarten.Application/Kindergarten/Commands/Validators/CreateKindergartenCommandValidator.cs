using FluentValidation;
using Kindergarten.Application.Common.Validators.Kindergarten;

namespace Kindergarten.Application.Kindergarten.Commands.Validators;

public class CreateKindergartenCommandValidator : AbstractValidator<CreateKindergartenCommand>
{
    public CreateKindergartenCommandValidator()
    {
        RuleFor(x => x.Dto)
            .SetValidator(new CreateKindergartenDtoValidator());
    }
}