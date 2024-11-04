using Microsoft.AspNetCore.Identity;

namespace Kindergarten.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public IList<ApplicationUserRole> UserRoles { get; } = new List<ApplicationUserRole>();
}