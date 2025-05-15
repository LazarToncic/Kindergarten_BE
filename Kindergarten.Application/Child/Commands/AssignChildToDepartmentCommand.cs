using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Child.Commands;

public record AssignChildToDepartmentCommand(Guid ChildId, Guid DepartmentId) : IRequest;

public class AssignChildToDepartmentCommandHandler(IChildrenService childrenService) : IRequestHandler<AssignChildToDepartmentCommand>
{
    public async Task Handle(AssignChildToDepartmentCommand request, CancellationToken cancellationToken)
    {
        await childrenService.AssignChildToDepartment(request.ChildId, request.DepartmentId, cancellationToken);
    }
}