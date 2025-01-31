namespace Kindergarten.Domain.Entities;

public class Salary
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EmployeeId { get; set; }
    public int Amount { get; set; }
    public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
    public int? Bonus { get; set; }
    public int? Tax { get; set; }
    public string Currency { get; set; } = "RSD";
    public string EmployeePosition { get; set; }

    public Employee Employee { get; set; }
}