using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.ChildWithdrawal.Commands;

public record ApproveChildWithdrawalCommand(Guid ChildRequestWithdrawalId) : IRequest;

public class ApproveChildWithdrawalCommandHandler(IChildWithdrawalService childWithdrawalService) : IRequestHandler<ApproveChildWithdrawalCommand>
{
    public async Task Handle(ApproveChildWithdrawalCommand request, CancellationToken cancellationToken)
    {
        await childWithdrawalService.ApproveChildWithdrawalsAsync(request.ChildRequestWithdrawalId, cancellationToken); 
    }
}