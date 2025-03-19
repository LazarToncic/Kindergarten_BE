namespace Kindergarten.Application.Common.Dto.Children;

public record NewChildrenThroughParentRequestDto(Guid Id, bool HasAllergies, List<string>? Allergies,
    bool HasMedicalIssues, List<string>? MedicalConditions);