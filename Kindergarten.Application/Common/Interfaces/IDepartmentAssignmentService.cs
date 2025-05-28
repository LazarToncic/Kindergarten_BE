namespace Kindergarten.Application.Common.Interfaces;

public interface IDepartmentAssignmentService
{
    Task DeactivateActiveAssignmentAsync(Guid childId, string performedByUserId, CancellationToken ct);
}