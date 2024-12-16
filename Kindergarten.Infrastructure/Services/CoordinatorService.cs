using Kindergarten.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class CoordinatorService(ICurrentUserService currentUserService, IKindergartenDbContext dbContext) : ICoordinatorService
{
    public async Task<bool> CheckIfCoordinatorWorksInSameKindergarten(string kindergartenName)
    {
        if (currentUserService.Roles!.Contains("Owner") || currentUserService.Roles!.Contains("Manager"))
            return true;

        if (currentUserService.Roles!.Contains("Coordinator"))
        {
            var existingKindergartenName = await dbContext.Users
                .Include(x => x.Employee)
                .ThenInclude(x => x.Kindergarten)
                .Where(x => x.Id.Equals(currentUserService.UserId))
                .Select(x => x.Employee.Kindergarten.Name)
                .FirstOrDefaultAsync();

            return existingKindergartenName == kindergartenName;
        }

        return false;
    }
}