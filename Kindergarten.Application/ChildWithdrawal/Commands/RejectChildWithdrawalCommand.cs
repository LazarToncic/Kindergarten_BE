using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.ChildWithdrawal.Commands;

public record RejectChildWithdrawalCommand(Guid ChildWithdrawalRequestId) : IRequest;

public class RejectChildWithdrawalCommandHandler(IChildWithdrawalService childWithdrawalService) : IRequestHandler<RejectChildWithdrawalCommand>
{
    public async Task Handle(RejectChildWithdrawalCommand request, CancellationToken cancellationToken)
    {
        await  childWithdrawalService.RejectChildWithdrawalsAsync(request.ChildWithdrawalRequestId, cancellationToken);
    }
}