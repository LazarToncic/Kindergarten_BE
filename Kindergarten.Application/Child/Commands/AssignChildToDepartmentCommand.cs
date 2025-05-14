using MediatR;

namespace Kindergarten.Application.Child.Commands;

public record AssignChildToDepartmentCommand(Guid ChildId, Guid DepartmentId) : IRequest;

public class AssignChildToDepartmentCommandHandler : IRequestHandler<AssignChildToDepartmentCommand>
{
    public Task Handle(AssignChildToDepartmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}