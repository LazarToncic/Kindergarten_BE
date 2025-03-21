namespace Kindergarten.Domain.Entities;

public class Department
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public int AgeGroup { get; set; } 
    public int Capacity { get; set; } 
    public string Name { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<KindergartenDepartment> KindergartenDepartments { get; set; } = new List<KindergartenDepartment>();
    public List<DepartmentEmployee> DepartmentEmployees { get; set; } = new();
    public List<ChildDepartment> ChildDepartmentAssignments { get; set; } = new List<ChildDepartment>();
}