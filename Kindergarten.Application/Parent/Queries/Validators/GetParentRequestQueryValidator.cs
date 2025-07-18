using FluentValidation;
using Kindergarten.Application.Common.Validators.Parent;

namespace Kindergarten.Application.Parent.Queries.Validators;

public class GetParentRequestQueryValidator : AbstractValidator<GetParentRequestsQuery>
{
    public GetParentRequestQueryValidator()
    {
        RuleFor(x => x.Dto)
            .SetValidator(new GetParentRequestQueryDtoValidator());
    }
}