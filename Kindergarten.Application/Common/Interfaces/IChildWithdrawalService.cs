using Kindergarten.Application.Common.Dto.ChildWithdrawalRequest;
using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Common.Interfaces;

public interface IChildWithdrawalService
{
    public Task CreateChildWithdrawalAsync(Guid childId, string? reason, CancellationToken cancellationToken );
    
    public Task<WithdrawalRequestDtoResponseList> GetChildWithdrawalsAsync(Guid? kindergartenId, string? firstName, string? lastName, ChildWithdrawalRequestStatus? status, CancellationToken cancellationToken );
    
    public Task RejectChildWithdrawalsAsync(Guid childWithdrawalRequestId, CancellationToken cancellationToken );
    public Task ApproveChildWithdrawalsAsync(Guid childWithdrawalRequestId, CancellationToken cancellationToken );
}