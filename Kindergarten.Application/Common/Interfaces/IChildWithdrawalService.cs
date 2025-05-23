namespace Kindergarten.Application.Common.Interfaces;

public interface IChildWithdrawalService
{
    public Task CreateChildWithdrawalAsync(Guid childId, string? reason, CancellationToken cancellationToken );
}