namespace Kindergarten.Application.Common.Interfaces;

public interface IParentChildService
{
    Task<IList<string>> DeactivateParentChildLinksAsync(Guid childId, string performedByUserId, CancellationToken ct);
}