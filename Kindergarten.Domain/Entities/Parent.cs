namespace Kindergarten.Domain.Entities;

public class Parent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public bool IsVerified { get; set; }
    public ApplicationUser User { get; set; }
    public List<ParentChild> ParentChildren { get; set; } = new List<ParentChild>();
}