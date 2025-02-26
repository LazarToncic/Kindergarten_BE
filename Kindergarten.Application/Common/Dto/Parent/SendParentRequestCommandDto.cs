namespace Kindergarten.Application.Common.Dto.Parent;

public record SendParentRequestCommandDto(int NumberOfChildren, string? AdditionalInfo, string PreferredKindergarten,List<ParentRequestChildDto> Children);