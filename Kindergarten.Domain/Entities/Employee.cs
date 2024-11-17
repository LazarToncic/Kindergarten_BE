namespace Kindergarten.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public Guid EmployeePositionId { get; set; }
    public Guid KindergartenId { get; set; }
    public DateTime HiredDate { get; set; } = DateTime.UtcNow;

    public EmployeePosition EmployeePosition { get; set; }
    public Kindergarten Kindergarten { get; set; }
    public ApplicationUser User { get; set; }
    public List<DepartmentEmployee> DepartmentEmployees { get; set; } = new();
    public List<Salary> Salaries { get; set; } = new List<Salary>();
    public List<EmployeeQualification> EmployeeQualifications { get; set; } = new();
}