namespace Kindergarten.Domain.Entities;

public class QualificationType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } 

    public List<Qualification> Qualifications { get; set; } = new();
}