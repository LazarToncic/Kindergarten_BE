namespace Kindergarten.Application.Common.Dto.Parent;

public record GetParentRequestQueryDto(string? FirstName, string? LastName, Guid KindergartenId, bool? IsOnlineApproved,
    bool? IsInPersonApproved, int PageNumber = 1, int PageSize = 10);