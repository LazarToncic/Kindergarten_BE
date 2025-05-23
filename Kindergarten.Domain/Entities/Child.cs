
namespace Kindergarten.Domain.Entities;

public class Child
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly YearOfBirth { get; set; }
    public Guid RequestedKindergartenId { get; set; }          
    public Kindergarten RequestedKindergarten { get; set; } = null!;
    public List<ParentChild> ParentChildren { get; set; } = new List<ParentChild>();
    public ICollection<ChildAllergy> ChildAllergies { get; set; } = new List<ChildAllergy>();
    public List<ChildMedicalCondition> ChildMedicalConditions { get; set; } = new List<ChildMedicalCondition>();
    public List<ChildDepartment> DepartmentAssignments { get; set; } = new List<ChildDepartment>();
    public ICollection<ChildWithdrawalRequest> WithdrawalRequests { get; set; }
        = new List<ChildWithdrawalRequest>();
}