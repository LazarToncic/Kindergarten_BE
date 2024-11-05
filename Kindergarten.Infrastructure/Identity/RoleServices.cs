using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Kindergarten.Infrastructure.Identity;

public class RoleServices(RoleManager<ApplicationRole> roleManager) : IRoleService
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
    
}