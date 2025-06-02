using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Parent;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;
using Kindergarten.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ParentService(ICurrentUserService currentUserService, IKindergartenDbContext dbContext, 
    ICoordinatorService coordinatorService, IKindergartenService kindergartenService, IChildrenService childrenService,
    ApplicationUserManager userManager) : IParentService
{
    public async Task CreateParentRequest(int numberOfChildren, string? additionalInfo, string preferredKindergarten, 
        ParentChildRelationship parentChildRelationship, List<ParentRequestChildDto> children, CancellationToken cancellationToken)
    {
        var parentRequest = ParentRequestMapper.NewParentRequest(currentUserService.UserId!, numberOfChildren, additionalInfo,
            preferredKindergarten, parentChildRelationship,children);
        
        dbContext.ParentRequests.Add(parentRequest);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetParentRequestQueryResponseDto> GetParentRequest(GetParentRequestQueryDto dto,
        CancellationToken cancellationToken)
    {
        var userRoles = currentUserService.Roles!;
        var kindergartenName = await kindergartenService.GetKindergartenName(dto.KindergartenId);
        
        IQueryable<ParentRequest> query = dbContext.ParentRequests.AsQueryable();
        

        if (userRoles.Contains(RolesExtensions.Manager) || userRoles.Contains(RolesExtensions.Owner))
        {
            query = query.Where(pr => pr.PreferredKindergarten == kindergartenName);
        }
        else if (userRoles.Contains(RolesExtensions.Coordinator))
        {
            var isUserCoordinatorForThisKindergarten =
                await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten2(kindergartenName);
        
        
            if (!isUserCoordinatorForThisKindergarten) 
                throw new CoordinatorException("You are not a coordinator for this Kindergarten and cant make any changes.");
            
            query = query.Where(pr => pr.PreferredKindergarten == kindergartenName);
        }else
        {
            throw new UnauthorizedAccessException("User role is not authorized to access this information.");
        }

        if (!string.IsNullOrWhiteSpace(dto.FirstName))
            query = query.Where(x => x.User.FirstName.Contains(dto.FirstName));

        if (!string.IsNullOrWhiteSpace(dto.LastName))
            query = query.Where(x => x.User.LastName.Contains(dto.LastName));

        if (dto.IsOnlineApproved.HasValue)
            query = query.Where(x => x.IsOnlineApproved == dto.IsOnlineApproved);

        if (dto.IsInPersonApproved.HasValue)
            query = query.Where(x => x.IsInPersonApproved == dto.IsInPersonApproved);

        var pagedRequests = await query
            .Include(pr => pr.User)
            .Include(pr => pr.OnlineApprovedByUser)
            .Include(pr => pr.InPersonApprovedByUser)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync(cancellationToken);
        
        var pagedRequestsDto = pagedRequests.ParentRequestToGetParentRequestQueryResponseDto();

        return pagedRequestsDto;
    }

    public async Task ApproveParentRequestOnline(Guid parentRequestId, CancellationToken cancellationToken)
    {
        var parentRequest = await GetParentRequestById(parentRequestId);
        var canUserApproveThis =
            await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten(parentRequest.PreferredKindergarten);

        if (!canUserApproveThis)
            throw new PermissionDeniedException("User does not have permission to approve this request.");
        
        parentRequest.IsOnlineApproved = true;
        parentRequest.OnlineApprovedByUserId = currentUserService.UserId;

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ApproveParentRequestInPerson(Guid parentRequestId, CancellationToken cancellationToken)
    {
        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var parentRequest = await GetParentRequestById(parentRequestId);
        
            var canUserApproveThis =
                await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten(parentRequest.PreferredKindergarten);

            if (!canUserApproveThis)
                throw new PermissionDeniedException("User does not have permission to approve this request.");
            
            if (!parentRequest.IsOnlineApproved)
                throw new ParentRequestOnlineNotApprovedException("The request must be approved online before it can be approved in person.");
        
            parentRequest.IsInPersonApproved = true;
            parentRequest.InPersonApprovedByUserId = currentUserService.UserId;

            CheckIfParentRequestIsFullyApproved(parentRequest);

            await dbContext.SaveChangesAsync(cancellationToken);

            await ApproveRequest(parentRequest.UserId, cancellationToken);

            var parentId = await CreateParentThroughApprovedParentRequest(parentRequest.UserId, cancellationToken);
            
            await childrenService.AddChildrenThroughParentRequest(parentRequest.ChildrenJson, parentId,parentRequest.ParentRole, parentRequest.PreferredKindergarten, cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveParentRoleForOrphanedAsync(IEnumerable<string> orphanedParentUserIds, CancellationToken cancellationToken)
    {
        foreach (var orphanedParentUserId in orphanedParentUserIds)
        {
            var user = await userManager.FindByIdAsync(orphanedParentUserId);
            if (user == null)
                continue;

            if (await userManager.IsInRoleAsync(user, RolesExtensions.Parent))
            {
                var result = await userManager.RemoveFromRoleAsync(user, RolesExtensions.Parent);
                    
                if (!result.Succeeded)
                    throw new UnableToDeleteRoleException($"Neuspešno uklanjanje role 'Parent' za korisnika {orphanedParentUserId}: " +
                                                          string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }

    public async Task DeactivateParentAsync(IList<string> parentUserIds, string performedByUserId, CancellationToken cancellationToken)
    {
        var parents = await dbContext.Parents
            .Where(x => parentUserIds.Contains(x.UserId))
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);

        foreach (var parent in parents)
        {
            parent.IsActive = false;
            parent.DeletedAt = DateTime.Now;
            parent.DeletedByUserId = performedByUserId;
        }
    }

    public async Task<Guid> GetParentIdWithUserId(string userId, CancellationToken cancellationToken)
    {
        if (userId == null)
            throw new NotFoundException("User id cannot be null.");
        
        var parentId = await dbContext.Parents
            .Where(x => x.UserId == userId)
            .Select(x => x.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (parentId == null)
            throw new NotFoundException("Parent id cannot be null.");
        
        return Guid.Parse(parentId);
    }

    private async Task<Guid> CreateParentThroughApprovedParentRequest(string userId, CancellationToken cancellationToken)
    {
        var parent = new Parent
        {
            UserId = userId
        };
        
        dbContext.Parents.Add(parent);
        await dbContext.SaveChangesAsync(cancellationToken);

        return parent.Id;
    }

    private void CheckIfParentRequestIsFullyApproved(ParentRequest parentRequest)
    {
        if (parentRequest is { IsInPersonApproved: true, IsOnlineApproved: true }) 
            parentRequest.ApprovedAt = DateTime.UtcNow;
    }

    private async Task ApproveRequest(string userId, CancellationToken cancellationToken)
    {
        var parentRoleId = await dbContext.Roles
            .Where(x => x.Name!.Equals(RolesExtensions.Parent))
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (parentRoleId == null)
            throw new NotFoundException("Ova rola ne postoji");

        var userRole = new ApplicationUserRole
        {
            RoleId = parentRoleId,
            UserId = userId
        };
        
        dbContext.UserRoles.Add(userRole);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<ParentRequest> GetParentRequestById(Guid parentRequestId)
    {
        var parentRequest = await dbContext.ParentRequests
            .FirstOrDefaultAsync(x => x.Id == parentRequestId);
        
        if (parentRequest == null)
            throw new NotFoundException("Parent request not found.");

        return parentRequest;
    }
}