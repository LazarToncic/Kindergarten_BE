namespace Kindergarten.Domain.Entities;

public class MedicalCondition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    
    public List<ChildMedicalCondition> ChildMedicalConditions { get; set; } = new List<ChildMedicalCondition>();
}