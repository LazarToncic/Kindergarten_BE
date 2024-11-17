namespace Kindergarten.Domain.Entities;

public class EmployeeQualification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime QualificationObtained { get; set; }
    public Employee Employee { get; set; }
    public Guid EmployeeId { get; set; }
    public Qualification Qualification { get; set; }
    public Guid QualificationId { get; set; }
}