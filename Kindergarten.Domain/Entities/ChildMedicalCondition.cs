namespace Kindergarten.Domain.Entities;

public class ChildMedicalCondition
{
    public Guid ChildId { get; set; }
    public Child Child { get; set; }
    
    public Guid MedicalConditionId { get; set; }
    public MedicalCondition MedicalCondition { get; set; }
}