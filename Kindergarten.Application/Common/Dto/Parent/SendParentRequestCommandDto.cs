using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Dto.Parent;

public record SendParentRequestCommandDto(
    int NumberOfChildren,
    ParentChildRelationship ParentChildRelationship,
    string? AdditionalInfo, 
    string PreferredKindergarten,
    List<ParentRequestChildDto> Children
    );