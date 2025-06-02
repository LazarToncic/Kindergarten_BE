using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Interfaces;

public interface IChildrenService
{
    Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId, ParentChildRelationship relationship, string preferredKindergarten, CancellationToken cancellationToken);
    
    Task AddNewChild(string firstName, string lastName, DateOnly dateOfBirth, bool hasAllergies,
        List<string>? allergies, bool hasMedicalIssues, List<string>? medicalConditions,
        ParentChildRelationship parentChildRelationship,Guid preferredKindergartenId, CancellationToken cancellationToken);

    Task<GetUnassignedChildrenListDto> GetUnassignedChildren(Guid? kindergartenId, string? firstName, string? lastName,
        CancellationToken cancellationToken);
    
    Task DeactivateChildAsync(Guid childId, string deletedByUserId , CancellationToken cancellationToken);
    
    Task<int> GetChildrenAge(Guid childrenId, CancellationToken cancellationToken);

    Task AssignChildToDepartment(Guid childId, Guid departmentId, CancellationToken cancellationToken);

    Task<GetChildrenQueryResponseList> GetChildren(Guid? kindergartenId, string? firstName, string? lastName, DateOnly? dateOfBirth, bool? isActive, CancellationToken cancellationToken);
}