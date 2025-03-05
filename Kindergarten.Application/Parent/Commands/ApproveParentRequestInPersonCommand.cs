using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Parent.Commands;

public record ApproveParentRequestInPersonCommand(Guid ParentRequestId) : IRequest;

public class ApproveParentRequestInPersonCommandHandler(IParentService parentService) : IRequestHandler<ApproveParentRequestInPersonCommand>
{
    public async Task Handle(ApproveParentRequestInPersonCommand request, CancellationToken cancellationToken)
    {
        await parentService.ApproveParentRequestInPerson(request.ParentRequestId, cancellationToken);
    }
}