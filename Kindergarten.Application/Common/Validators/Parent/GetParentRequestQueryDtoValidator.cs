using FluentValidation;
using Kindergarten.Application.Common.Dto.Parent;

namespace Kindergarten.Application.Common.Validators.Parent;

public class GetParentRequestQueryDtoValidator : AbstractValidator<GetParentRequestQueryDto>
{
    public GetParentRequestQueryDtoValidator()
    {
        RuleFor(x => x.KindergartenId)
            .NotEmpty().WithMessage("KindergartenId must not be empty.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.");

        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("First name must be at most 50 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters long.")
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));
    }
}