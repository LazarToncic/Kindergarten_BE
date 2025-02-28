namespace Kindergarten.Application.Common.Dto.Parent;

public record ParentRequestSingleResponseDto(
    Guid Id,
    string FirstName,
    string LastName,
    string KindergartenName,
    bool IsOnlineApproved,
    bool IsInPersonApproved,
    DateTime CreatedAt,
    List<ParentRequestChildDto> Children
    );