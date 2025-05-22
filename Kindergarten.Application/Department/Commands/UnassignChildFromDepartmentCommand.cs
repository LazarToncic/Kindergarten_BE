using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Department.Commands;

public record UnassignChildFromDepartmentCommand(Guid ChildId, Guid DepartmentId) : IRequest;

public class UnassignChildFromDepartmentCommandHandler(IDepartmentService departmentService) : IRequestHandler<UnassignChildFromDepartmentCommand>
{
    public async Task Handle(UnassignChildFromDepartmentCommand request, CancellationToken cancellationToken)
    {
        await departmentService.UnassignChildFromDepartment(request.ChildId, request.DepartmentId, cancellationToken);
    }
}