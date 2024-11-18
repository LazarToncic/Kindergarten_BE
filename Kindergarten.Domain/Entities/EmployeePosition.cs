namespace Kindergarten.Domain.Entities;

public class EmployeePosition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<Employee> Employees { get; set; } = new List<Employee>();
}