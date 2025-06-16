namespace Kindergarten.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Expires { get; set; } = DateTime.UtcNow.AddDays(7);
    public bool IsRevoked { get; set; } = false; 
    
    public DateTime? RevokedAt { get; set; }
    public string? RevokedByIp { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}