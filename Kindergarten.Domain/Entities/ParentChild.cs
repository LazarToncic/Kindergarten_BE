using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Domain.Entities;

public class ParentChild
{
    public Guid ParentId { get; set; }
    public Parent Parent { get; set; }
    
    public Guid ChildId { get; set; }
    public Child Child { get; set; }
    public ParentChildRelationship Relationship { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? DeletedAt { get; set; }
    public string? DeletedByUserId { get; set; }
}