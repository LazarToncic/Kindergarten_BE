using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Interfaces;

public interface IChildrenService
{
    Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId, ParentChildRelationship relationship, CancellationToken cancellationToken);

    Task AddNewChild(string firstName, string lastName, DateOnly dateOfBirth, bool hasAllergies,
        List<string>? allergies, bool hasMedicalIssues, List<string>? medicalConditions,
        ParentChildRelationship parentChildRelationship,CancellationToken cancellationToken);
}