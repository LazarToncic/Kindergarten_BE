using Kindergarten.Application.Common.Dto.ChildWithdrawalRequest;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.ChildWithdrawal;
using Kindergarten.Application.Common.Repositories;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ChildWithdrawalService(IKindergartenDbContext dbContext ,ICurrentUserService currentUserService,
    IDepartmentEmployeeRepository departmentEmployeeRepository, ICoordinatorService coordinatorService,
    IDepartmentAssignmentService departmentAssignmentService, IParentChildService parentChildService, IParentService parentService,
    IChildrenService childrenService) : IChildWithdrawalService
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

    public async Task<WithdrawalRequestDtoResponseList> GetChildWithdrawalsAsync(Guid? kindergartenId, string? firstName,
        string? lastName,
        ChildWithdrawalRequestStatus? status, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId!;
        var roles = currentUserService.Roles;

        var query = dbContext.ChildWithdrawalRequests
            .AsNoTracking()
            .Include(x => x.Child)
                .ThenInclude(x => x.ParentChildren)
                .ThenInclude(x => x.Parent)
                .ThenInclude(x => x.User)
            .Include(x => x.RequestedByUser)
            .Include(x => x.ReviewedByUser)
            .Include(x => x.Child)
                .ThenInclude(x => x.DepartmentAssignments)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.DepartmentEmployees)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.User)
            .Include(r => r.Child)
                .ThenInclude(c => c.DepartmentAssignments)
                .ThenInclude(da => da.Kindergarten)
            .Where(x => true);

        if (roles!.Contains(RolesExtensions.Owner) || roles!.Contains(RolesExtensions.Manager))
        {
            if (kindergartenId.HasValue)
            {
                query = query
                    .Where(r => r.Child.DepartmentAssignments.Any(da => da.KindergartenId == kindergartenId.Value));
            }
        } else if (roles.Contains(RolesExtensions.Coordinator))
        {
            var kindergartenIdCoordinator = await coordinatorService.GetKindergartenIdForCoordinator(userId, cancellationToken);
            query = query.Where(r => r.Child.DepartmentAssignments.Any(da => da.KindergartenId == kindergartenIdCoordinator));
        }
        else
        {
            throw new UnauthorizedAccessException("You dont have access.");
        }
        
        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(r => r.Child.FirstName.Contains(firstName));
        
        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(r => r.Child.LastName.Contains(lastName));
        
        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        var entities = await query.ToListAsync(cancellationToken);

        return entities.ToDtoList();
    }

    public async Task RejectChildWithdrawalsAsync(Guid childWithdrawalRequestId, CancellationToken cancellationToken)
    {
        var childRequest = await dbContext.ChildWithdrawalRequests
            .Include(x => x.Child)
            .ThenInclude(x => x.DepartmentAssignments)
            .Where(x => x.Id == childWithdrawalRequestId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (childRequest == null)
            throw new NotFoundException("ChildRequest not found.");
        
        var activeKgId = childRequest.Child.DepartmentAssignments
            .First(da => da.IsActive).KindergartenId;

        if (!await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten(
                activeKgId, cancellationToken))
            throw new UnauthorizedAccessException("You dont have access to do this.");

        childRequest.Status = ChildWithdrawalRequestStatus.Rejected;
        childRequest.ReviewedByUserId = currentUserService.UserId!;
        childRequest.ReviewedAt = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ApproveChildWithdrawalsAsync(Guid childWithdrawalRequestId, CancellationToken cancellationToken)
    {
        var childRequest = await dbContext.ChildWithdrawalRequests
            .Include(x => x.Child)
            .ThenInclude(x => x.DepartmentAssignments)
            .Where(x => x.Id == childWithdrawalRequestId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (childRequest == null)
            throw new NotFoundException("ChildRequest not found.");
        
        var activeKgId = childRequest.Child.DepartmentAssignments
            .First(da => da.IsActive).KindergartenId;

        if (!await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten(
                activeKgId, cancellationToken))
            throw new UnauthorizedAccessException("You dont have access to do this.");
        
        await using var tx = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            childRequest.Status           = ChildWithdrawalRequestStatus.Approved;
            childRequest.ReviewedByUserId = currentUserService.UserId!;
            childRequest.ReviewedAt       = DateTime.UtcNow;

            await departmentAssignmentService.DeactivateActiveAssignmentAsync(childRequest.ChildId, currentUserService.UserId!, cancellationToken);
            var orphanedParentIds = await parentChildService.DeactivateParentChildLinksAsync(childRequest.ChildId, currentUserService.UserId!, cancellationToken);

            if (orphanedParentIds.Any())
            {
                await parentService.RemoveParentRoleForOrphanedAsync(orphanedParentIds, cancellationToken);
                await parentService.DeactivateParentAsync(orphanedParentIds, currentUserService.UserId! ,cancellationToken);
            }
            
            await childrenService.DeactivateChildAsync(childRequest.ChildId, currentUserService.UserId!, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await tx.RollbackAsync(cancellationToken);
            Console.WriteLine("Greška prilikom odobravanja povlačenja djeteta: " + ex.ToString());
            throw;
        }
        
        
    }
}