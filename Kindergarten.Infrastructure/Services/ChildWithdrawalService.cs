using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Repositories;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ChildWithdrawalService(IKindergartenDbContext dbContext ,ICurrentUserService currentUserService,
    IDepartmentEmployeeRepository departmentEmployeeRepository) : IChildWithdrawalService
{
    public async Task CreateChildWithdrawalAsync(Guid childId, string? reason, CancellationToken cancellationToken)
    {
        var child = await dbContext.Children
            .Include(c => c.DepartmentAssignments)
            .FirstOrDefaultAsync(c => c.Id == childId, cancellationToken);

        if (child == null)
            throw new NotFoundException($"Child {childId} not found.");
        
        var already = await dbContext.ChildWithdrawalRequests
            .AnyAsync(r => r.ChildId == childId && r.Status == ChildWithdrawalRequestStatus.Pending, cancellationToken);
        
        if (already)
            throw new ConflictException("There is already a pending withdrawal request for this child.");

        var activeAssignment = child.DepartmentAssignments
            .FirstOrDefault(da => da.IsActive);

        if (activeAssignment == null)
            throw new ChildAssignemntNotActiveException("Child is not active in any department.");

        var userId = currentUserService.UserId!;
        var isTeacher = await departmentEmployeeRepository
            .IsTeacherInDepartmentAsync(
                activeAssignment.DepartmentId,
                userId,
                activeAssignment.KindergartenId,
                cancellationToken);

        if (!isTeacher)
            throw new TeacherNotTeachingDepartmentException("Only the teacher of the department can submit a withdrawal request.");

        var childWithdrawalRequest = new ChildWithdrawalRequest
        {
            ChildId = childId,
            RequestedByUserId = userId,
            Reason = reason
        };

        dbContext.ChildWithdrawalRequests.Add(childWithdrawalRequest);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}