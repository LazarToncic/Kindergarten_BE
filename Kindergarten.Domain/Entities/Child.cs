
namespace Kindergarten.Domain.Entities;

public class Child
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime YearOfBirth { get; set; }
    public List<ParentChild> ParentChildren { get; set; } = new List<ParentChild>();
    public ICollection<ChildAllergy> ChildAllergies { get; set; } = new List<ChildAllergy>();
}