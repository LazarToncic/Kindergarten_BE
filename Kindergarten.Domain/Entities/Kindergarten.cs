namespace Kindergarten.Domain.Entities;

public class Kindergarten
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } 
    public string Address { get; set; } 
    public string City { get; set; }
    public string ContactPhone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Department> Departments { get; set; } = new List<Department>();
}