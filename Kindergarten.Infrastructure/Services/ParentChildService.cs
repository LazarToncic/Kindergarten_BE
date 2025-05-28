using Kindergarten.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ParentChildService(IKindergartenDbContext dbContext) : IParentChildService
{
    public async Task<IList<string>> DeactivateParentChildLinksAsync(Guid childId, string performedByUserId, CancellationToken ct)
    {
        var links = await dbContext.ParentChildren
            .Where(x => x.ChildId == childId && x.IsActive == true)
            .ToListAsync(ct);

        foreach (var link in links)
        {
            link.IsActive = false;
            link.DeletedAt = DateTime.UtcNow;
            link.DeletedByUserId = performedByUserId;
        }

        var parentIds = links
            .Select(x => x.ParentId)
            .Distinct()
            .ToList();
        
        var orphaned = new List<string>();

        foreach (var parentId in parentIds)
        {
            var hasKids = await dbContext.ParentChildren
                .AnyAsync(pc => pc.ParentId == parentId && pc.IsActive, ct);
            if (!hasKids)
                orphaned.Add(parentId.ToString());
        }
        
        return orphaned;
    }
}