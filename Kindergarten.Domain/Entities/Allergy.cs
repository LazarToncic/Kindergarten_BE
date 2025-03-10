namespace Kindergarten.Domain.Entities;

public class Allergy
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    
    public ICollection<ChildAllergy> ChildAllergies { get; set; } = new List<ChildAllergy>();
}