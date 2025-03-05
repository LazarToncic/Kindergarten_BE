using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Parent.Commands;

public record ApproveParentRequestOnlineCommand(Guid ParentRequestId) : IRequest;

public class ApproveParentRequestOnlineCommandHandler(IParentService parentService) : IRequestHandler<ApproveParentRequestOnlineCommand>
{
    public async Task Handle(ApproveParentRequestOnlineCommand request, CancellationToken cancellationToken)
    {
        await parentService.ApproveParentRequestOnline(request.ParentRequestId, cancellationToken);
    }
}