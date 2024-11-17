namespace Kindergarten.Domain.Entities;

public class Qualification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public List<EmployeeQualification> EmployeeQualifications { get; set; } = new();
}