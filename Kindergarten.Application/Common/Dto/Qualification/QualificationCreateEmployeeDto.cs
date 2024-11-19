namespace Kindergarten.Application.Common.Dto.Qualification;

public record QualificationCreateEmployeeDto(string Qualification, string? Description, DateTime QualificationObtained, string TypeOfQualification);