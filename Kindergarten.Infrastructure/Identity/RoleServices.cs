using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kindergarten.Infrastructure.Identity;

public class RoleServices(RoleManager<ApplicationRole> roleManager, IKindergartenDbContext dbContext) : IRoleService
{
    public async Task CreateRoleAsync(string role)
    {
        var alreadyExists = await roleManager.RoleExistsAsync(role);

        if (!alreadyExists)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = role,
                NormalizedName = role.Normalize(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }
    }

    public async Task AddRoleToEmployeeAsync(string userId, string newRole, CancellationToken cancellationToken)
    {
        if (newRole == "Coordinator")
        {
            var role = await roleManager.FindByNameAsync(newRole);

            if (role == null)
                throw new NotFoundException("This role does not exist");

            var userRole = new ApplicationUserRole
            {
                RoleId = role.Id,
                UserId = userId
            };
            
            dbContext.UserRoles.Add(userRole);
        }
        else
        {
            var employeeRole = await roleManager.FindByNameAsync("Employee");
            
            if (employeeRole == null)
                throw new NotFoundException("This role does not exist");

            var userRole = new ApplicationUserRole
            {
                RoleId = employeeRole.Id,
                UserId = userId
            };
            
            dbContext.UserRoles.Add(userRole);
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}