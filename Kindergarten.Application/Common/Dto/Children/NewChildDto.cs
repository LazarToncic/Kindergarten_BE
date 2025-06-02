namespace Kindergarten.Application.Common.Dto.Children;

public record NewChildDto(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    bool HasAllergies, 
    List<string>? Allergies,
    bool HasMedicalIssues, 
    List<string>? MedicalConditions,
    Guid PreferredKindergartenId
    );