namespace Kindergarten.Domain.Entities;

public class KindergartenDepartment
{
    public Kindergarten Kindergarten { get; set; }
    public Department Department { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid KindergartenId { get; set; }
}