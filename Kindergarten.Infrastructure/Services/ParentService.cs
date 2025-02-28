using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Parent;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ParentService(ICurrentUserService currentUserService, IKindergartenDbContext dbContext, 
    ICoordinatorService coordinatorService) : IParentService
{
    public async Task CreateParentRequest(int numberOfChildren, string? additionalInfo, string preferredKindergarten, 
        List<ParentRequestChildDto> children, CancellationToken cancellationToken)
    {
        var parentRequest = ParentRequestMapper.NewParentRequest(currentUserService.UserId!, numberOfChildren, additionalInfo,
            preferredKindergarten, children);
        
        dbContext.ParentRequests.Add(parentRequest);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetParentRequestQueryResponseDto> GetParentRequest(GetParentRequestQueryDto dto,
        CancellationToken cancellationToken)
    {
        var userRoles = currentUserService.Roles!;
        IQueryable<ParentRequest> query = dbContext.ParentRequests.AsQueryable();

        if (userRoles.Contains(RolesExtensions.Manager) || userRoles.Contains(RolesExtensions.Owner))
        {
            query = query.Where(pr => pr.PreferredKindergarten == dto.KindergartenName);
        }
        else if (userRoles.Contains(RolesExtensions.Coordinator))
        {
            var isUserCoordinatorForThisKindergarten =
                await coordinatorService.CheckIfCoordinatorWorksInSameKindergarten2(dto.KindergartenName);
        
        
            if (!isUserCoordinatorForThisKindergarten) 
                throw new CoordinatorException("You are not a coordinator for this Kindergarten and cant make any changes.");
            
            query = query.Where(pr => pr.PreferredKindergarten == dto.KindergartenName);
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

        // ovde vrati samo 1 element iako ima 2
        var pagedRequests = await query
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync(cancellationToken);
        
        // ovde bude null
        var pagedRequestsDto = pagedRequests.ParentRequestToGetParentRequestQueryResponseDto();

        return pagedRequestsDto;
    }
}