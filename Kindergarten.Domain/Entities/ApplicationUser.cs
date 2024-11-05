using Microsoft.AspNetCore.Identity;

namespace Kindergarten.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int YearOfBirth { get; set; }
    public IList<ApplicationUserRole> Roles { get; } = new List<ApplicationUserRole>();
    
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    
}