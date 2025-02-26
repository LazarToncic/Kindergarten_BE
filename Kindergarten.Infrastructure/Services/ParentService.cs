using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Parent;

namespace Kindergarten.Infrastructure.Services;

public class ParentService(ICurrentUserService currentUserService, IKindergartenDbContext dbContext) : IParentService
{
    public async Task CreateParentRequest(int numberOfChildren, string? additionalInfo, string preferredKindergarten, 
        List<ParentRequestChildDto> children, CancellationToken cancellationToken)
    {
        var parentRequest = ParentRequestMapper.NewParentRequest(currentUserService.UserId!, numberOfChildren, additionalInfo,
            preferredKindergarten, children);
        
        dbContext.ParentRequests.Add(parentRequest);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}