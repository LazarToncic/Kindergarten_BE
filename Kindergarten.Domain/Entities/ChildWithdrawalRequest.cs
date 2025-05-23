namespace Kindergarten.Domain.Entities;

public enum ChildWithdrawalRequestStatus
{
    Pending,
    Approved,
    Rejected 
}

public class ChildWithdrawalRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ChildId { get; set; }
    public Child Child { get; set; } = null!;
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public string RequestedByUserId { get; set; } = null!;
    public string? Reason { get; set; }
    public ChildWithdrawalRequestStatus Status { get; set; } = ChildWithdrawalRequestStatus.Pending;
    public string? ReviewedByUserId { get; set; }
    public DateTime? ReviewedAt { get; set; }
}