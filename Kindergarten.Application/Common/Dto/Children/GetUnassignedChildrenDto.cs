namespace Kindergarten.Application.Common.Dto.Children;

public record GetUnassignedChildrenDto(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    bool HasAllergies, 
    List<string>? Allergies,
    bool HasMedicalIssues, 
    List<string>? MedicalConditions
    );