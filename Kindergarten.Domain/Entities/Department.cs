namespace Kindergarten.Domain.Entities;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public int AgeGroup { get; set; } 
    public int Capacity { get; set; } 
    public string Name { get; set; } 
    public string TeacherId { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid KindergartenId { get; set; }
    public Kindergarten Kindergarten { get; set; } 
}