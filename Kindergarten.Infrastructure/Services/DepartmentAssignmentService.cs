using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class DepartmentAssignmentService(IKindergartenDbContext dbContext) : IDepartmentAssignmentService
{
    public async Task DeactivateActiveAssignmentAsync(Guid childId, string performedByUserId, CancellationToken ct)
    {
        var assignment = await dbContext.ChildDepartments
            .FirstOrDefaultAsync(x => x.ChildId == childId && x.IsActive == true, ct);

        if (assignment == null)
            throw new NotFoundException("Child department does not exist");
        
        assignment.IsActive = false;
        assignment.UnassignedAt = DateTime.UtcNow;
        assignment.AssignedByUserId = performedByUserId;
    }
}