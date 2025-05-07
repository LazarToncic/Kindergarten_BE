namespace Kindergarten.Domain.Entities;

public class ChildDepartment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid ChildId { get; set; }
    public Child Child { get; set; } = null!;
    
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    
    public string AssignedByUserId { get; set; } = string.Empty;
    
    public bool IsActive { get; set; } = true;
    public Guid KindergartenId { get; set; }
    public Kindergarten Kindergarten { get; set; } = null!;
}