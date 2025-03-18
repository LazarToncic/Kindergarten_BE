using System.Runtime.InteropServices.JavaScript;

namespace Kindergarten.Domain.Entities;

public class Parent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public DateTime VerifiedAt { get; set; } = DateTime.UtcNow;
    public ApplicationUser User { get; set; }
    public List<ParentChild> ParentChildren { get; set; } = new List<ParentChild>();
}