namespace Kindergarten.Domain.Entities;

public class DepartmentEmployee
{
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }

    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public string RoleInDepartment { get; set; }
    public bool IsActive  { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}