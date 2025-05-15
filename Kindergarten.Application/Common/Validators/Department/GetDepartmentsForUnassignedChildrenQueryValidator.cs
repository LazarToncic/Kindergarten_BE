using FluentValidation;
using Kindergarten.Application.Department.Queries;

namespace Kindergarten.Application.Common.Validators.Department;

public class GetDepartmentsForUnassignedChildrenQueryValidator : AbstractValidator<GetDepartmentsForUnassignedChildrenQuery>
{
    public GetDepartmentsForUnassignedChildrenQueryValidator()
    {
        RuleFor(x => x.ChildId).NotEmpty()
            .WithMessage("ChildId cannot be empty");
    }
}