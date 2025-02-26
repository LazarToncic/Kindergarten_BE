namespace Kindergarten.Domain.Entities;
public class ParentRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public int NumberOfChildren { get; set; }
    public string? AdditionalInfo { get; set; }
    public bool IsOnlineApproved { get; set; }
    public bool IsInPersonApproved { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedAt { get; set; }
    public string PreferredKindergarten { get; set; }
    public string ChildrenJson { get; set; } = string.Empty;
    
    public ApplicationUser User { get; set; } = null!;
    
}