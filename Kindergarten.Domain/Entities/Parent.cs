namespace Kindergarten.Domain.Entities;

public class Parent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public bool IsVerified { get; set; }
    public List<ParentChild> ParentChildren { get; set; } = new List<ParentChild>();
}