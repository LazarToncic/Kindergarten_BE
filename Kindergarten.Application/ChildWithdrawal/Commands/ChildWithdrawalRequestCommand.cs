using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.ChildWithdrawal.Commands;

public record ChildWithdrawalRequestCommand(Guid ChildId, string? Reason) : IRequest;

public class ChildWithdrawalRequestCommandHandler(IChildWithdrawalService childWithdrawalService) : IRequestHandler<ChildWithdrawalRequestCommand>
{
    public async Task Handle(ChildWithdrawalRequestCommand request, CancellationToken cancellationToken)
    {
        await childWithdrawalService.CreateChildWithdrawalAsync(request.ChildId, request.Reason, cancellationToken);
    }
}