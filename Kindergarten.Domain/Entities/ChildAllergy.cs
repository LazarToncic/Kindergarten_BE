namespace Kindergarten.Domain.Entities;

public class ChildAllergy
{
    public Guid ChildId { get; set; }
    public Child Child { get; set; } = null!;
    
    public Guid AllergyId { get; set; }
    public Allergy Allergy { get; set; } = null!;
}