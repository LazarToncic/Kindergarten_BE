using Microsoft.AspNetCore.Identity;

namespace Kindergarten.Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationUser User { get; set; }
    public ApplicationRole Role { get; set; }
}