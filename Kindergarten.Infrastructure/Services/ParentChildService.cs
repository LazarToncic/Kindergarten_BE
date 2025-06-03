using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ParentChildService(IKindergartenDbContext dbContext) : IParentChildService
{
    public async Task<IList<string>> DeactivateParentChildLinksAsync(Guid childId, string performedByUserId, CancellationToken ct)
    {
        var links = await dbContext.ParentChildren
            .Include(x => x.Parent)
            .Where(x => x.ChildId == childId && x.IsActive)
            .ToListAsync(ct);

        var deactivatedLinkIds = new List<Guid>();
        foreach (var link in links)
        {
            link.IsActive = false;
            link.DeletedAt = DateTime.UtcNow;
            link.DeletedByUserId = performedByUserId;
            deactivatedLinkIds.Add(link.Id);
        }

        var parentIds = links
            .Select(x => x.ParentId)
            .Distinct()
            .ToList();

        var orphaned = new List<string>();

        foreach (var parentId in parentIds)
        {
            var parentUserId = links
                .Where(x => x.ParentId == parentId)
                .Select(x => x.Parent.UserId)
                .FirstOrDefault();

            if (parentUserId == null)
                throw new NotFoundException("Parent is not found");

            var hasOtherKids = await dbContext.ParentChildren
                .AnyAsync(pc =>
                        pc.ParentId == parentId &&
                        pc.IsActive && 
                        !deactivatedLinkIds.Contains(pc.Id)
                    , ct);

            if (!hasOtherKids)
                orphaned.Add(parentUserId);
        }
        
        return orphaned;
    }
}