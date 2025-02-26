namespace Kindergarten.Application.Common.Dto.Parent;

public record ParentRequestChildDto(string FirstName, string LastName, DateTime DateOfBirth, bool HasAllergies, List<string>? Allergies,
    bool HasMedicalIssues, List<string>? MedicalConditions);