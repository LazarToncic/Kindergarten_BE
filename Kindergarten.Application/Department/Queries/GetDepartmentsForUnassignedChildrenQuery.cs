using Kindergarten.Application.Common.Dto.Department;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Department.Queries;

public record GetDepartmentsForUnassignedChildrenQuery(Guid ChildId) : IRequest<DepartmentsForUnassignedChildrenListDto>;

public class GetDepartmentsForUnassignedChildrenQueryHandler(IDepartmentService departmentService) : IRequestHandler<GetDepartmentsForUnassignedChildrenQuery, DepartmentsForUnassignedChildrenListDto>
{
    public async Task<DepartmentsForUnassignedChildrenListDto> Handle(GetDepartmentsForUnassignedChildrenQuery request, CancellationToken cancellationToken)
    {
        return await departmentService.GetDepartmentsForUnassignedChildrenList(request.ChildId, cancellationToken);
    }
}