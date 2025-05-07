using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
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

    public async Task<bool> CheckIfCoordinatorWorksInSameKindergarten2(string kindergartenName)
    {
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

    public async Task<bool> CheckIfEmployeeIsBeingPromotedToCoordinator(Guid coordinatorId)
    {
        return await dbContext.EmployeePositions
            .AnyAsync(x => x.Id == coordinatorId && x.Name == "Coordinator");
    }

    public async Task<Guid> GetKindergartenIdForCoordinator(string userId, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .Include(e => e.EmployeePosition)
            .FirstOrDefaultAsync(e =>
                    e.UserId == userId &&
                    e.EmployeePosition.Name == EmployeeExtension.Coordinator, cancellationToken);

        if (employee == null)
            throw new NotFoundException("Coordinator not found.");

        return employee.KindergartenId;
    }
}